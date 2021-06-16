using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace API_Gateway_Service.HubConfig
{
    public class NotificationHub : Hub
    {
        public void sendToAll(string notification)
        {
            Clients.All.SendAsync("sendToNotification", notification);
        }

        public async Task JoinRoom(string roomName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessage(string notification, string groupName)
        {
            //await Clients.Group(groupName).SendAsync("Send",mess);
            await Clients.Group(groupName).SendAsync("ReceiveNotification", notification);
        }


    }
}