using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Command_Service.HubConfig;

namespace Command_Service.Service
{
    public class CommandService : ICommandService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public CommandService(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }
        public async Task<string> setTimeInterval(int interval)
        {
            using (var httpClient = new HttpClient())
            {
                var c = JsonConvert.SerializeObject(interval);
                StringContent content = new StringContent(c, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://Sensor_Device_Service:80/api/smartHome/interval", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    _ = _hub.Clients.Group("notificationGroup").SendAsync("ReceiveNotification", "Interval is seted to: "+interval);
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
                using (var response = await httpClient.GetAsync("http://Sensor_Device_Service:80/api/smartHome/interval"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return apiResponse;
                }

            }
        }
    }
}
