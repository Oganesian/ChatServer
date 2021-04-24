using AccountAndConnection;
using ChatClient.AuxiliaryClasses;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using ChatData;
using CryptographyServices.KeyExchangeServices;
using Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ChatClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Singleton
        private static MainWindowViewModel instance;
        public static MainWindowViewModel GetInstance(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService)
        {
            if (instance == null)
            {
                instance = new MainWindowViewModel(authenticator, keyExchangeService);
            }
            return instance;
        }

        public static MainWindowViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;

        private readonly MainWindowModel model;

        #region Model reference types
        public Account _account;

        private string _fullUsername;
        private List<ChatItemContainer> _chats;
        private ObservableCollection<ChatView> _chatsToDisplay;
        private ChatView _currentChat;
        #endregion

        #region Model Properties
        public Account Account
        {
            get => model.Account;
            set
            {
                model.Account = value;
                SetProperty(ref _account, value);
            }
        }
        public string FullUsername
        {
            get => model.FullUsername;
            set
            {
                model.FullUsername = value;
                SetProperty(ref _fullUsername, value);
            }
        }

        public List<ChatItemContainer> Chats
        {
            get => _chats;
            set => SetProperty(ref _chats, value);

        }

        public ObservableCollection<ChatView> ChatsToDisplay
        {
            get => model.ChatsToDisplay;
            set
            {
                model.ChatsToDisplay = value;
                SetProperty(ref _chatsToDisplay, value);
            }
        }

        public ChatView CurrentChat
        {
            get => model.CurrentChat;
            set
            {
                model.CurrentChat = value;
                SetProperty(ref _currentChat, value);
            }
        }
        #endregion

        private MainWindowViewModel(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService)
        {
            _authenticator = authenticator;
            _keyExchangeService = keyExchangeService;
            model = MainWindowModel.GetInstance();
            Account = new Account(_keyExchangeService, _authenticator.CurrentAccout);

            if (Account != null)
            {
                Account.MessageReceivedCallback = ReceiveMessageAsync;
                DisplayChats();
            }
        }

        private void DisplayChats()
        {
            Chats = new List<ChatItemContainer>();
            foreach (var chat in Account.Chats)
            {
                Chats.Add(ChatItemContainerFactory.Create(chat));
                ChatsToDisplay.Add(Chats[^1].ChatItem); // TODO: wtf
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void AddReceivedMessageToTargetChat(ChatItemContainer targetChat, Message message)
        {
            targetChat.ViewModel.AddMessage(message);
        }

        public void SaveAccountChat(int receiverUniqueId)
        {
            var chat = Account.Chats.Find(x => x.receiverUniqueId == receiverUniqueId);
            if (chat != null)
            {
                JsonSerializerProvider.SerializeChat(Account.Username, Account.PublicId, chat);
            }
        }
    }
}