using API_Gateway_Service.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway_Service.Service
{
    public class GatewayService : IGatewayService
    {
        public async Task<string> AddDataFromSensors(SmartHome smartHome)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(smartHome);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://Data_Service:80/api/smartHomeData", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }

            }
        }

        public async Task<IEnumerable<SmartHome>> GetByUse(float use, string grSmUse)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(null);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://Data_Service:80/api/smartHomeData/usage?use="+ use + "&grSmUse="+ grSmUse))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<SmartHome>>(apiResponse);
                }

            }
        }

        public async Task<IEnumerable<SmartHome>> GetByGen(float gen, string grSmGen)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(null);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://Data_Service:80/api/smartHomeData/generated?gen=" + gen + "&grSmGen=" + grSmGen))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<SmartHome>>(apiResponse);
                }

            }
        }

        public async Task<IEnumerable<SmartHome>> GetByTemp(float temp, string grSmTemp)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(null);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://Data_Service:80/api/smartHomeData/temperature?temp=" + temp + "&grSmTemp=" + grSmTemp))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<SmartHome>>(apiResponse);
                }

            }
        }

        public async Task<IEnumerable<SmartHome>> GetAll(int from, int to)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(null);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://Data_Service:80/api/smartHomeData/allData?from=" + from + "&to=" + to))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<SmartHome>>(apiResponse);
                }

            }
        }

        public async Task<string> setTimeInterval(int interval)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(interval);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://Command_Service:80/api/command/setTimeInterval", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }

            }
        }

        public async Task<string> getTimeInterval()
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(null);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://Command_Service:80/api/command/getTimeInterval"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }

            }
        }

        public async Task<string> getLastAction()
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(null);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.GetAsync("http://Command_Service:80/api/command/getLastAction"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }

            }
        }
    }
}
