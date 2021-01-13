using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.Serialization;
using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly MainWindowModel model;

        #region Model reference types
        private Client _client;
        
        private ObservableCollection<MyTabItem> _friends;
        #endregion

        #region Model Properties
        public Client Client
        {
            get
            {
                return model.Client;
            }
            set
            {
                model.Client = value;
                SetProperty(ref _client, value);
            }
        }

        public ObservableCollection<MyTabItem> Friends
        {
            get
            {
                return model.Friends;
            }
            set
            {
                model.Friends = value;
                SetProperty(ref _friends, value);
            }
        }
        #endregion

        #region Commands
        private ICommand _connectToServer;
        #endregion

        public MainWindowViewModel()
        {
            model = MainWindowModel.GetInstance();
            //Messages.Add(MessageUserControlFactory.Create(MessageType.INCOMING, "hello world"));
            //Messages.Add(MessageUserControlFactory.Create(MessageType.OUTGOING, "hey mate"));
            //Messages.Add(MessageUserControlFactory.Create(MessageType.INCOMING, "what's up?"));

            if (Client != null)
            {
                Client.Chats = LoadClientChats();
                //JsonSerializerProvider.SerializeClient(Client);
                DisplayChats();
            }
        }

        private void DisplayChats()
        {
            foreach (var chat in Client.Chats)
            {
                Friends.Add(MyTabItemFactory.Create(chat));
            }
        }

        private List<Chat> LoadClientChats()
        {
            return JsonSerializerProvider.DeserializeClientChats(Client);
        }


        #region Commands Getters
        public ICommand ConnectToServer
        {
            get
            {
                if (_connectToServer == null)
                {
                    _connectToServer = new CommandHandler(() => ConectToServerExec(), () => CanExecute);
                }
                return _connectToServer;
            }
        }
        #endregion

        private void ConectToServerExec()
        {

        }
    }
}