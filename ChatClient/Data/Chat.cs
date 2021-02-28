using ChatClient.ClientConnection;
using ChatClient.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatClient.Data
{
    [Serializable()]
    public class Chat
    {
        public string senderUsername;
        public string senderId;
        public int senderUniqueId;
        public string receiverUsername;
        public string receiverId;
        public int receiverUniqueId;
        public ObservableCollection<Message> messages;
        public DateTime lastMessageDateTime;
    }
}
