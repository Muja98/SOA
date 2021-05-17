using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GemBox.Spreadsheet;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Data_Service.Model;

using System.IO;
using ExcelDataReader;

namespace Data_Service.Service
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
                //string filePath = "C:/Users/Stefan-PC/Desktop/SOA/SOA/SOA/SOA_PROJEKAT_1/SOA_PROJEKAT_1/Data_Service/Data/SOA_DATA.xlsx";
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
                                for(int i=0; i<reader.FieldCount; i++)
                                {
                                    if (reader.GetValue(i) == null)
                                    {
                                        currentRow += " ,";
                                    }
                                    else
                                    {
                                        currentRow += reader.GetValue(i).ToString()+",";
                                    }
                                }

                                SmartHome newSmartHomeObject = mapToModel(currentRow);
                                Object smartHomeObject = setSensorType(newSmartHomeObject);
                                _logger.LogInformation(smartHomeObject.ToString());
                                int timeInterval = StaticClasses.SmartHomeStaticData.timeInterval * 1000;
                                await Task.Delay(timeInterval, stoppingToken);
                            }
                           
                        } while (reader.NextResult());
                    }
                }
            }
        }

        public SmartHome mapToModel(string dataArray)
        {
            string[] splitedDataArray = dataArray.Split(",");
            SmartHome newSmartHomeObject = new SmartHome();
            newSmartHomeObject.Time =               splitedDataArray[0]==" "?0:Convert.ToInt32(splitedDataArray[0]);
            newSmartHomeObject.Use =                splitedDataArray[1] == " " ? 0 : Convert.ToDouble(splitedDataArray[1]);
            newSmartHomeObject.Gen =                splitedDataArray[2] == " " ? 0 : Convert.ToDouble(splitedDataArray[2]);
            newSmartHomeObject.HouseOverall =       splitedDataArray[3] == " " ? 0 : Convert.ToDouble(splitedDataArray[3]);
            newSmartHomeObject.Dishwasher =         splitedDataArray[4] == " " ? 0 : Convert.ToDouble(splitedDataArray[4]);
            newSmartHomeObject.Furnace1 =           splitedDataArray[5] == " " ? 0 : Convert.ToDouble(splitedDataArray[5]);
            newSmartHomeObject.Furnace2 =           splitedDataArray[6] == " " ? 0 : Convert.ToDouble(splitedDataArray[6]);
            newSmartHomeObject.HomeOffice =         splitedDataArray[7] == " " ? 0 : Convert.ToDouble(splitedDataArray[7]);
            newSmartHomeObject.Fridge =             splitedDataArray[8] == " " ? 0 : Convert.ToDouble(splitedDataArray[8]);
            newSmartHomeObject.WineCellar =         splitedDataArray[9] == " " ? 0 : Convert.ToDouble(splitedDataArray[9]);
            newSmartHomeObject.GarageDoor =         splitedDataArray[10] == " " ? 0 : Convert.ToDouble(splitedDataArray[10]);
            newSmartHomeObject.Kitchen1 =           splitedDataArray[11] == " " ? 0 : Convert.ToDouble(splitedDataArray[11]);
            newSmartHomeObject.Kitchen2 =           splitedDataArray[12] == " " ? 0 : Convert.ToDouble(splitedDataArray[12]);
            newSmartHomeObject.Kitchen3 =           splitedDataArray[13] == " " ? 0 : Convert.ToDouble(splitedDataArray[13]);
            newSmartHomeObject.Barn =               splitedDataArray[14] == " " ? 0 : Convert.ToDouble(splitedDataArray[14]);
            newSmartHomeObject.Well =               splitedDataArray[15] == " " ? 0 : Convert.ToDouble(splitedDataArray[15]);
            newSmartHomeObject.Microwave =          splitedDataArray[16] == " " ? 0 : Convert.ToDouble(splitedDataArray[16]);
            newSmartHomeObject.LivingRoom =         splitedDataArray[17] == " " ? 0 : Convert.ToDouble(splitedDataArray[17]);
            newSmartHomeObject.Solar =              splitedDataArray[18] == " " ? 0 : Convert.ToDouble(splitedDataArray[18]);
            newSmartHomeObject.Temperature =        splitedDataArray[19] == " " ? 0 : Convert.ToDouble(splitedDataArray[19]);
            newSmartHomeObject.Icon =               splitedDataArray[20];
            newSmartHomeObject.Humidity =           splitedDataArray[21] == " " ? 0 : Convert.ToDouble(splitedDataArray[21]);
            newSmartHomeObject.Visibility =         splitedDataArray[22] == " " ? 0 : Convert.ToDouble(splitedDataArray[22]);
            newSmartHomeObject.Summary =            splitedDataArray[23];
            newSmartHomeObject.ApparentTemperature= splitedDataArray[24] == " " ? 0 : Convert.ToDouble(splitedDataArray[24]);
            newSmartHomeObject.Pressure =           splitedDataArray[25] == " " ? 0 : Convert.ToDouble(splitedDataArray[25]);
            newSmartHomeObject.WindSpeed =          splitedDataArray[26] == " " ? 0 : Convert.ToDouble(splitedDataArray[26]);
            newSmartHomeObject.CloudCover =         splitedDataArray[27];
            newSmartHomeObject.WindBearing =        splitedDataArray[28] == " " ? 0 : Convert.ToDouble(splitedDataArray[28]);
            newSmartHomeObject.PrecipIntensity =    splitedDataArray[29] == " " ? 0 : Convert.ToDouble(splitedDataArray[29]);
            newSmartHomeObject.DewPoint =           splitedDataArray[30] == " " ? 0 : Convert.ToDouble(splitedDataArray[30]);
            newSmartHomeObject.PrecipProbability =  splitedDataArray[31] == " " ? 0 : Convert.ToDouble(splitedDataArray[31]);
            return newSmartHomeObject;
        }

        public Object setSensorType(SmartHome smartHome)
        {
            if (StaticClasses.SmartHomeStaticData.sensorType == 1)
            {
                return smartHome;
            }
            else if (StaticClasses.SmartHomeStaticData.sensorType == 2)
            {
                SmartHomeElectricityDto electricityDto = new SmartHomeElectricityDto();
                electricityDto.Time = smartHome.Time;
                electricityDto.Use = smartHome.Use;
                electricityDto.Gen = smartHome.Gen;
                electricityDto.HouseOverall = smartHome.HouseOverall;
                electricityDto.Dishwasher = smartHome.Dishwasher;
                electricityDto.Furnace1 = smartHome.Furnace1;
                electricityDto.Furnace2 = smartHome.Furnace2;
                electricityDto.HomeOffice = smartHome.HomeOffice;
                electricityDto.Fridge = smartHome.Fridge;
                electricityDto.WineCellar = smartHome.WineCellar;
                electricityDto.GarageDoor = smartHome.GarageDoor;
                electricityDto.Kitchen1 = smartHome.Kitchen1;
                electricityDto.Kitchen2 = smartHome.Kitchen2;
                electricityDto.Kitchen3 = smartHome.Kitchen3;
                electricityDto.Barn = smartHome.Barn;
                electricityDto.Well = smartHome.Well;
                electricityDto.Microwave = smartHome.Microwave;
                electricityDto.LivingRoom = smartHome.LivingRoom;
                electricityDto.Solar = smartHome.Solar;
                return electricityDto;
            }
            else {
                SmartHomeSensorsExceptElectricityDto otherSensors = new SmartHomeSensorsExceptElectricityDto();
                otherSensors.Time = smartHome.Time;
                otherSensors.Temperature = smartHome.Temperature;
                otherSensors.Icon = smartHome.Icon;
                otherSensors.Humidity = smartHome.Humidity;
                otherSensors.Visibility = smartHome.Visibility;
                otherSensors.Summary = smartHome.Summary;
                otherSensors.ApparentTemperature = smartHome.ApparentTemperature;
                otherSensors.Pressure = smartHome.Pressure;
                otherSensors.WindSpeed = smartHome.WindSpeed;
                otherSensors.CloudCover = smartHome.CloudCover;
                otherSensors.WindBearing = smartHome.WindBearing;
                otherSensors.PrecipIntensity = smartHome.PrecipIntensity;
                otherSensors.DewPoint = smartHome.DewPoint;
                otherSensors.PrecipProbability = smartHome.PrecipProbability;
                return otherSensors;
            }
   
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Work Stoped.");
            return Task.CompletedTask;
        }
    }
}
