
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using Sensor_Device_Service.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

using Nancy.Json;

namespace Sensor_Device_Service.Service
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

                string host = "xz04k8.messaging.internetofthings.ibmcloud.com";
                string clientid = "d:xz04k8:Sensors:SmartHomeSensor";
                string username = "use-token-auth";
                string password = "BKDBj*Uhqemri5t8ge";
                string topic = "iot-2/evt/smarthomesensor/fmt/json";

                MqttClient mQttClient = new MqttClient(host);
                mQttClient.Connect(clientid, username, password);
                string filePath = "./Data/SOA_DATA.xlsx";
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (!stoppingToken.IsCancellationRequested && reader.Read())
                            {
                                string currentRow = "";
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (reader.GetValue(i) == null)
                                    {
                                        currentRow += " ,";
                                    }
                                    else
                                    {
                                        currentRow += reader.GetValue(i).ToString() + ",";
                                    }
                                }

                                SmartHome newSmartHomeObject = mapToModel(currentRow);
                                var json = new JavaScriptSerializer().Serialize(newSmartHomeObject);
                                mQttClient.Publish(topic, Encoding.UTF8.GetBytes(json));
                                _logger.LogInformation(json);
     
                                await Task.Delay(2500, stoppingToken);
                            }

                        } while (reader.NextResult());
                    }
                }
                mQttClient.Disconnect();
            }
        }

        public SmartHome mapToModel(string dataArray)
        {
            string[] splitedDataArray = dataArray.Split(",");

            SmartHome newSmartHomeObject = new SmartHome();

            if (splitedDataArray[0] != " ")
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(splitedDataArray[0]));
                DateTime dateTimeCounted = dateTimeOffset.DateTime;
                newSmartHomeObject.Time = dateTimeCounted;
            }
            else
            {

                DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                newSmartHomeObject.Time = epoch;
            }


            newSmartHomeObject.Use = splitedDataArray[1] == " " ? 0 : Convert.ToDouble(splitedDataArray[1]);
            newSmartHomeObject.Gen = splitedDataArray[2] == " " ? 0 : Convert.ToDouble(splitedDataArray[2]);
            newSmartHomeObject.HouseOverall = splitedDataArray[3] == " " ? 0 : Convert.ToDouble(splitedDataArray[3]);
            newSmartHomeObject.Dishwasher = splitedDataArray[4] == " " ? 0 : Convert.ToDouble(splitedDataArray[4]);
            newSmartHomeObject.Furnace1 = splitedDataArray[5] == " " ? 0 : Convert.ToDouble(splitedDataArray[5]);
            newSmartHomeObject.Furnace2 = splitedDataArray[6] == " " ? 0 : Convert.ToDouble(splitedDataArray[6]);
            newSmartHomeObject.HomeOffice = splitedDataArray[7] == " " ? 0 : Convert.ToDouble(splitedDataArray[7]);
            newSmartHomeObject.Fridge = splitedDataArray[8] == " " ? 0 : Convert.ToDouble(splitedDataArray[8]);
            newSmartHomeObject.WineCellar = splitedDataArray[9] == " " ? 0 : Convert.ToDouble(splitedDataArray[9]);
            newSmartHomeObject.GarageDoor = splitedDataArray[10] == " " ? 0 : Convert.ToDouble(splitedDataArray[10]);
            newSmartHomeObject.Kitchen1 = splitedDataArray[11] == " " ? 0 : Convert.ToDouble(splitedDataArray[11]);
            newSmartHomeObject.Kitchen2 = splitedDataArray[12] == " " ? 0 : Convert.ToDouble(splitedDataArray[12]);
            newSmartHomeObject.Kitchen3 = splitedDataArray[13] == " " ? 0 : Convert.ToDouble(splitedDataArray[13]);
            newSmartHomeObject.Barn = splitedDataArray[14] == " " ? 0 : Convert.ToDouble(splitedDataArray[14]);
            newSmartHomeObject.Well = splitedDataArray[15] == " " ? 0 : Convert.ToDouble(splitedDataArray[15]);
            newSmartHomeObject.Microwave = splitedDataArray[16] == " " ? 0 : Convert.ToDouble(splitedDataArray[16]);
            newSmartHomeObject.LivingRoom = splitedDataArray[17] == " " ? 0 : Convert.ToDouble(splitedDataArray[17]);
            newSmartHomeObject.Solar = splitedDataArray[18] == " " ? 0 : Convert.ToDouble(splitedDataArray[18]);
            newSmartHomeObject.Temperature = splitedDataArray[19] == " " ? 0 : Convert.ToDouble(splitedDataArray[19]);
            newSmartHomeObject.Icon = splitedDataArray[20];
            newSmartHomeObject.Humidity = splitedDataArray[21] == " " ? 0 : Convert.ToDouble(splitedDataArray[21]);
            newSmartHomeObject.Visibility = splitedDataArray[22] == " " ? 0 : Convert.ToDouble(splitedDataArray[22]);
            newSmartHomeObject.Summary = splitedDataArray[23];
            newSmartHomeObject.ApparentTemperature = splitedDataArray[24] == " " ? 0 : Convert.ToDouble(splitedDataArray[24]);
            newSmartHomeObject.Pressure = splitedDataArray[25] == " " ? 0 : Convert.ToDouble(splitedDataArray[25]);
            newSmartHomeObject.WindSpeed = splitedDataArray[26] == " " ? 0 : Convert.ToDouble(splitedDataArray[26]);
            newSmartHomeObject.CloudCover = splitedDataArray[27];
            newSmartHomeObject.WindBearing = splitedDataArray[28] == " " ? 0 : Convert.ToDouble(splitedDataArray[28]);
            newSmartHomeObject.PrecipIntensity = splitedDataArray[29] == " " ? 0 : Convert.ToDouble(splitedDataArray[29]);
            newSmartHomeObject.DewPoint = splitedDataArray[30] == " " ? 0 : Convert.ToDouble(splitedDataArray[30]);
            newSmartHomeObject.PrecipProbability = splitedDataArray[31] == " " ? 0 : Convert.ToDouble(splitedDataArray[31]);
            return newSmartHomeObject;
        }

        
      
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Work Stoped.");
            return Task.CompletedTask;
        }


    }
}
