using ChatClient.Data;
using ChatClient.ViewModels;
using ChatClient.Views;
using System;

namespace ChatClient.Factories
{
    public class MessageUserControlFactory
    {
        public static IMessageUserControl Create(MessageType type, string text)
        {
            string timestamp = DateTime.Now.ToString("HH:mm");
            UserControlMessageViewModel dataContext = new UserControlMessageViewModel(text, timestamp);

            return type switch
            {
                MessageType.INCOMING => new UserControlMessageReceived { DataContext = dataContext },
                MessageType.OUTGOING => new UserControlMessageSent { DataContext = dataContext },
                _ => new UserControlMessageSent(),
            };
        }
    }
}
