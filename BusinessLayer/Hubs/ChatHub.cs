using BusinessLayer.IHubs;
using DataLayer.Models;
using DataLayer.Models.Auth;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        //Qachonki bog'lansa
        /*public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }*/

        //Qachonki bog'lana olmasa
        /*public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }*/
        /*public async Task SendMessageToAll(string fromName, string sentText)
        {
            var message = new
            {
                SenderName = fromName,
                TextOfSender = sentText,
                SentAt = DateTimeOffset.UtcNow
            };

            await Clients.All.SendAsync("ReceiveMessage", message.SenderName, message.TextOfSender, message.SentAt);

        }*/

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task AllNotifications()
        {
            await Clients.All.AllNotificationMessages();
        }

        public async Task AllUsers()
        {
            await Clients.All.AllUser();
        }

        public async Task SendMyTextMessage(Message message)
        {
            /*var users = new string[] { message.FromMessage.ToString(), message.ToMessage.ToString() };*/
            await Clients.All.SendTextMessage(message);

        }



    }
}
