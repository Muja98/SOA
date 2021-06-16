using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Data_Service.Entities;

namespace Data_Service.HubConfig
{
    public class DataFromSensorHub : Hub
    {
        public void sendToAll(SmartHome sm)
        {
            Clients.All.SendAsync("sendToDashBoard", sm);
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessage(SmartHome mess, string groupName)
        {
            //await Clients.Group(groupName).SendAsync("Send",mess);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", mess);
        }


    }
}