using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sidechatr
{
    public class ChatUserUpdate
    {
        public DateTime Timestamp { get; set; }
        public ChatUser User { get; set; }
    }
}
