using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Command_Service.HubConfig;

namespace Command_Service.Service
{
    public class DataReader : IHostedService
    {
        public readonly ILogger<DataReader> _logger;
        private readonly ICommandService _commandService;
        private readonly IHubContext<NotificationHub> _hub;
        public DataReader(ILogger<DataReader> logger, ICommandService commandService, IHubContext<NotificationHub> hub)
        {
            this._logger = logger;
            this._commandService = commandService;
            _hub = hub;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Work Started.");
            await DoWork(cancellationToken);
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            if (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                ConnectionFactory factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
                factory.UserName = "guest";
                factory.Password = "guest";
                IConnection conn = factory.CreateConnection();
                IModel channel = conn.CreateModel();
                channel.QueueDeclare(queue: "analyticstocommand",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string message = System.Text.Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received analytics data from Rabbit reader: {0}", message);
                    int msg = int.Parse(message);
                    if (msg == 1)
                    {
                        string result = await _commandService.setTimeInterval(3);
                        StaticClasses.CurrentAction.currentAction = result;
                        _ = _hub.Clients.Group("notificationGroup").SendAsync("ReceiveNotification", "Interval is seted by siddhi to: " + 3);

                    }
                    else {
                        string result = await _commandService.setTimeInterval(5);
                        _ = _hub.Clients.Group("notificationGroup").SendAsync("ReceiveNotification", "Interval is seted by siddhi to: " + 5);

                    }
                };
                channel.BasicConsume(queue: "analyticstocommand",
                                        autoAck: true,
                                        consumer: consumer);
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Work Stoped.");
            return Task.CompletedTask;
        }

        public async Task sendCommandToDeviceService(int interval)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(interval);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://Sensor_Device_Service:80/api/smartHome/interval", content))//localhost:9604
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _logger.LogInformation(apiResponse);
                }

            }
        }
    }
}
