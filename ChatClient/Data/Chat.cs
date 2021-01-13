using ChatClient.ClientConnection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatClient.Data
{
    [Serializable()]
    public class Chat
    {
        public int uniqueId;
        public ObservableCollection<Message> messages;
        public DateTime lastMessageDateTime;
    }
}
