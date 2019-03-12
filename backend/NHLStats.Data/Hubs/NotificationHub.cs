using Microsoft.AspNetCore.SignalR;

namespace NHLStats.Data.Hubs
{
    public class NotificationHub : Hub
    {
        public void BroadcastNotification(string name, string message) 
        {
            Clients.All.SendAsync("broadcastNotification", name, message);
        }
        
    }
}
