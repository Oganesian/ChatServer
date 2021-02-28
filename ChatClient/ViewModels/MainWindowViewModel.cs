using ChatClient.AuxiliaryClasses;
using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.Serialization;
using ChatClient.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
        #endregion

        #region Commands
        private ICommand _connectToServer;
        #endregion

        private MainWindowViewModel() // TODO: set as private
        {
            model = MainWindowModel.GetInstance();

            Chats = new List<MyTabItemContainer>();
            if (Client != null)
            {
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
            }
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