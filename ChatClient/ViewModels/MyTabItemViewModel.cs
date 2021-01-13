using ChatClient.Data;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChatClient.ViewModels
{
    public class MyTabItemViewModel : BindableBase
    {
        private readonly MyTabItemModel model;
        private ObservableCollection<IMessageUserControl> _messages;
        private string _username;

        public MyTabItemViewModel(Chat chat)
        {
            model = new MyTabItemModel();
            DisplayChatMessages(chat);
            Username = "Username#" + chat.uniqueId; // TODO: remove
        }

        public ObservableCollection<IMessageUserControl> Messages
        {
            get
            {
                return model.Messages;
            }
            set
            {
                model.Messages = value;
                SetProperty(ref _messages, value);
            }
        }

        public string Username
        {
            get
            {
                return model.Username;
            }
            set
            {
                model.Username = value;
                SetProperty(ref _username, value);
            }
        }

        private void DisplayChatMessages(Chat chat)
        {
            foreach (var message in chat.messages)
            {
                Messages.Add(MessageUserControlFactory.Create(message));
            }
        }
    }
}
