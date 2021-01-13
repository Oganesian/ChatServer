using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatClient.Models
{
    public class MyTabItemModel
    {
        public ObservableCollection<IMessageUserControl> Messages { get; set; }
        public string Username { get; set; }

        public MyTabItemModel()
        {
            Messages = new ObservableCollection<IMessageUserControl>();
        }
    }
}
