using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sidechatr
{
    public class ChatUser
    {
        /// <summary>
        /// The id of the ChatUser. Must be unique.
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// The unique and consistent token by which the user can be identified on the client and server.
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// The friendly name of the ChatUser.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// A url to an image of the ChatUser.
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// The signalr connectionId associated with this ChatUser.
        /// </summary>
        public string connectionId { get; set; }
        /// <summary>
        /// Connection status of the Chatuser.
        /// </summary>
        public ChatConnectionStatus status { get; set; }


    }

    public enum ChatConnectionStatus
    {
        Active,
        Inactive,
        Offline
    };
}
