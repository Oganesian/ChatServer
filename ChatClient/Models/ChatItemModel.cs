using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatClient.Models
{
    public class ChatItemModel
    {
        public ObservableCollection<IMessageUserControl> Messages { get; set; }
        public string Username { get; set; }

        public ChatItemModel()
        {
            Messages = new ObservableCollection<IMessageUserControl>();
        }
    }
}
