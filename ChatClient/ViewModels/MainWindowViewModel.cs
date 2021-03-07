using ChatClient.AuxiliaryClasses;
using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.Serialization;
using ChatClient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Singleton
        private static MainWindowViewModel instance;
        public static MainWindowViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainWindowViewModel();
            }
            return instance;
        }
        #endregion

        private readonly MainWindowModel model;

        #region Model reference types
        private Client _client;
        private string _fullUsername;
        private List<MyTabItemContainer> _chats;
        private ObservableCollection<MyTabItem> _chatsToDisplay;
        private MyTabItem _currentChat;
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
        public string FullUsername

        {
            get
            {
                return model.FullUsername;
            }
            set
            {
                model.FullUsername = value;
                SetProperty(ref _fullUsername, value);
            }
        }

        public List<MyTabItemContainer> Chats
        {
            get
            {
                return _chats;
            }
            set
            {
                SetProperty(ref _chats, value);
            }
        }

        public ObservableCollection<MyTabItem> ChatsToDisplay
        {
            get
            {
                return model.ChatsToDisplay;
            }
            set
            {
                model.ChatsToDisplay = value;
                SetProperty(ref _chatsToDisplay, value);
            }
        }

        public MyTabItem CurrentChat
        {
            get
            {
                return model.CurrentChat;
            }
            set
            {
                model.CurrentChat = value;
                SetProperty(ref _currentChat, value);
            }
        }
        #endregion

        #region Commands
        private ICommand _connectToServer;
        #endregion

        private MainWindowViewModel()
        {
            model = MainWindowModel.GetInstance();

            Chats = new List<MyTabItemContainer>();

            if (Client != null)
            {
                Client.MessageReceived = ReceiveMessageAsync;
                Client.Chats = LoadClientChats();
                DisplayChats();
            }
        }

        private void DisplayChats()
        {
            foreach (var chat in Client.Chats)
            {
                Chats.Add(MyTabItemContainerFactory.Create(chat));
                ChatsToDisplay.Add(Chats[^1].MyTabItem); // TODO: Check if it works
                //ChatsToDisplay.Add(MyTabItemContainerFactory.Create(chat).MyTabItem);
            }
        }

        private async Task ReceiveMessageAsync(Message message)
        {
            try
            {
                var targetChat = Chats.Find(x => x.UniqueId == message.SenderUniqueId);
                message.Type = MessageType.INCOMING;
                //targetChat.ViewModel.AddMessage(message);
                //await Task.Run(() => targetChat.ViewModel.AddMessage(message));
                await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() => targetChat.ViewModel.AddMessage(message)));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void AddReceivedMessageToTargetChat(MyTabItemContainer targetChat, Message message)
        {
            targetChat.ViewModel.AddMessage(message);
        }

        private List<Chat> LoadClientChats()
        {
            return JsonSerializerProvider.DeserializeClientChats(Client);
        }

        public void SaveClientChat(int receiverUniqueId)
        {
            var chat = Client.Chats.Find(x => x.receiverUniqueId == receiverUniqueId);
            if(chat != null)
            {
                JsonSerializerProvider.SerializeChat(Client, chat);
            }
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