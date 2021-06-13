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

namespace Command_Service.Service
{
    public class DataReader : IHostedService
    {
        public readonly ILogger<DataReader> _logger;
        public DataReader(ILogger<DataReader> logger)
        {
            this._logger = logger;
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
                        await sendCommandToDeviceService(3);
                    }
                    else {
                        await sendCommandToDeviceService(5);
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
                    //return new JsonResult(
                    //    new
                    //    {
                    //        resp = apiResponse
                    //    }
                    //);
                    _logger.LogInformation(apiResponse);
                }

            }
        }
    }
}
