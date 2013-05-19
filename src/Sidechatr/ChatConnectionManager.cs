using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Sidechatr
{
    public static class ChatConnectionManager
    {
        private static readonly ConcurrentDictionary<string, ChatUser> users;
        private static readonly ConcurrentDictionary<string, ChatUser> usersByToken;
        private static readonly ChatConnectionManagerConfiguration configuration;

        private static Timer timer;

        static ChatConnectionManager()
        {
            users = new ConcurrentDictionary<string, ChatUser>();
            configuration = new ChatConnectionManagerConfiguration();

            timer = new Timer();
            timer.Interval = 5000;
            timer.AutoReset = true;
            timer.Start();
            timer.Elapsed += OnHeartbeat;
        }

        static void OnHeartbeat(object sender, ElapsedEventArgs e)
        {
            foreach (var user in users)
            {
                
            }
        }


        internal static List<ChatUser> RetrieveFriends(HubCallerContext context)
        {
            var token = context.QueryString["token"];
            var friends = configuration.retrieveFriends(token);

            return friends.ToList();
        }

        /*
        public ChatConnectionManager()
        {
            this.configuration = new ChatConnectionManagerConfiguration();
            this.connectionToUserMap = new ConcurrentDictionary<string, ChatUser>();
            this.userToConnectionMap = new ConcurrentDictionary<string, string>();
        }
        */

        internal static bool Connection(string connectionId, string token)
        {
            ChatUser user;

            if (users.TryGetValue(token, out user))
            {
                user.connectionId = connectionId;
            }
            else
            {
                user = configuration.retrieveUser(token);
                user.connectionId = connectionId;
            }

            return users.TryAdd(token, user);
        }

        internal static ChatUser Connection(HubCallerContext context, dynamic clients)
        {
            var token = context.QueryString["token"];
            var user = configuration.retrieveUser(token);
            user.connectionId = context.ConnectionId;
            users.TryAdd(token, user);
            return user;
        }

        internal static void Disconnect(string connectionId, ChatUser user)
        {
            ChatUser u;
            //this.connectionToUserMap.TryRemove(connectionId, out u);
        }

        internal static ChatUser UserFromConnectionId(string connectionId)
        {
            ChatUser user = null;
            //this.connectionToUserMap.TryGetValue(connectionId, out user);
            return user;
        }

        public class ChatConnectionManagerConfiguration
        {
            public ChatConnectionManagerConfiguration ForFriends(Func<string, IEnumerable<ChatUser>> retrieveFriends) 
            {
                this.retrieveFriends = retrieveFriends;
                return this;
            }

            public ChatConnectionManagerConfiguration ForUser(Func<string, ChatUser> retrieveUser)
            {
                this.retrieveUser = retrieveUser;
                return this;
            }

            internal Func<string, IEnumerable<ChatUser>> retrieveFriends;
            internal Func<string, ChatUser> retrieveUser;
        }

        public static ChatConnectionManagerConfiguration Configure()
        {
            return configuration;
        }

    }
}
