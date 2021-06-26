using System;
using System.Collections.ObjectModel;

namespace ChatData
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

        public byte[] sharedPrivateKey;
    }
}
