using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using Sidechatr;

namespace WebApplication2
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHubs();

            var users = new List<ChatUser> {
                new ChatUser { name = "Jon" },
                new ChatUser { name = "Fred" },
                new ChatUser { name = "Tim" }
            };

            var rand = new Random(DateTime.Now.Millisecond);
            var i = rand.Next(0, users.Count - 1);
            var user = users[i];

            ChatConnectionManager
                .Configure()
                .ForUser(token =>
                {
                    user.token = token;
                    return user;
                })
                .ForFriends(token => {

                    var friends = new List<ChatUser>() {
                        new ChatUser() { name = "Sam", status = ChatConnectionStatus.Active },
                        new ChatUser() { name = "Jim", status = ChatConnectionStatus.Active },
                        new ChatUser() { name = "Tim", status = ChatConnectionStatus.Inactive },
                        new ChatUser() { name = "Jon", status = ChatConnectionStatus.Offline }
                    };

                    return friends;

                });

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}