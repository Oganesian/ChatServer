using ChatClient.Data;
using ChatClient.Models;
using ChatClient.Serialization;
using System.Collections.Generic;

namespace ChatClient.ClientConnection
{
    public class Account : BaseAccount
    {
        public List<Chat> Chats { get; private set; }

        public TcpClientContainer Client { get; private set; }

        public Account()
        {
            // LoadChats(); TODO: enable
        }

        public void LoadChats()
        {
            Chats = JsonSerializerProvider.DeserializeChats(Username, PublicId);
        }

        public void SendMessage(Message message)
        {
            Client.SendMessage(message);
        }
    }
}
