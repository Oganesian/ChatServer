using ChatClient.Data;
using ChatClient.ViewModels;
using ChatClient.Views;
using System;

namespace ChatClient.Factories
{
    public class MessageUserControlFactory
    {
        public static IMessageUserControl Create(Message msg)
        {
            UserControlMessageViewModel dataContext = new UserControlMessageViewModel(msg.EncryptedMessageString, msg.Timestamp);

            return msg.Type switch
            {
                MessageType.OUTGOING => new UserControlMessageSent { DataContext = dataContext },
                MessageType.INCOMING => new UserControlMessageReceived { DataContext = dataContext },
                _ => new UserControlMessageReceived(),
            };
        }
    }
}
