using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Sidechatr
{
    [ChatAuthorize]
    public class ChatHub : Hub
    {
        public void Send(string senderId, string recipientId, string message)
        {
            Clients.All.send(Context.ConnectionId, message);
        }

        public override Task OnConnected()
        {
            var user = ChatConnectionManager.Connection(Context, Clients);
            var friends = ChatConnectionManager.RetrieveFriends(Context);

            // send the connecting user their list of friends
            Clients.Caller.friendUpdates(friends);

            // send an update to all of the user's friends

            return Clients.All.joined(user.name, DateTime.Now.ToString());
        }

        public override Task OnDisconnected()
        {
            return Clients.All.leave(Context.ConnectionId, DateTime.Now.ToString());
        }

        public override Task OnReconnected()
        {
            return Clients.All.rejoined(Context.ConnectionId, DateTime.Now.ToString());
        }
    }
}
