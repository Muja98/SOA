using Analytics_Service.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Analytics_Service.Service
{
    public class DataReader : IHostedService
    {
        public readonly ILogger<DataReader> _logger;
        private readonly IAnalyticsRepository _repository;

        public DataReader(ILogger<DataReader> logger, IAnalyticsRepository repository)
        {
            this._logger = logger;
            _repository = repository;
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
                channel.QueueDeclare(queue: "hello",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string message = System.Text.Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received from Rabbit data reader: {0}", message);

                    double temp = double.Parse(message);
                    SmartHomeTemperature sht = new SmartHomeTemperature();
                    sht.temperature = temp;
                    await _repository.AddDataFromSensors(sht);

                    await sendDataToSinddhiApp(message);
                };
                channel.BasicConsume(queue: "hello",
                                        autoAck: true,
                                        consumer: consumer);
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Work Stoped.");
            return Task.CompletedTask;
        }

        public async Task sendDataToSinddhiApp(string message)
        {
            using (var httpClient = new HttpClient())
            {
                double temperature = double.Parse(message);
                Object temp = new { temperature = temperature };
                var c = JsonConvert.SerializeObject(temp);
                Console.WriteLine(c);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://192.168.0.104:8006/temperature", content))//localhost:9604
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
