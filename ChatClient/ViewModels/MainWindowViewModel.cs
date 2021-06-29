using AccountAndConnection;
using ChatClient.AuxiliaryClasses;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.States.Authenticators;
using ChatClient.Views;
using ChatData;
using CryptographyServices.DecryptionServices;
using CryptographyServices.EncryptionServices;
using CryptographyServices.KeyExchangeServices;
using MaterialDesignThemes.Wpf;
using Serialization;
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
        public static MainWindowViewModel GetInstance(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService, 
            IDiffieHellmanEncryptionService messageEncryptionService, IDiffieHellmanDecryptionService messageDecryptionService)
        {
            if (instance == null)
            {
                instance = new MainWindowViewModel(authenticator, keyExchangeService, messageEncryptionService, messageDecryptionService);
            }
            return instance;
        }

        public static MainWindowViewModel GetInstance()
        {
            return instance;
        }
        #endregion


        #region Commands
        private ICommand addNewFriend;
        public ICommand AddNewFriend => addNewFriend ??= new CommandHandler(async () => await PerformAddNewFriendAsync(), () => CanExecute);

        #endregion

        private readonly IAuthenticator _authenticator;
        private readonly IDiffieHellmanKeyExchangeService _keyExchangeService;
        private readonly IDiffieHellmanEncryptionService _messageEncryptionService;
        private readonly IDiffieHellmanDecryptionService _messageDecryptionService;

        private readonly MainWindowModel model;

        #region Model reference types
        public Account _account;

        private string _fullUsername;
        private List<ChatItemContainer> _chats;
        private ObservableCollection<ChatView> _chatsToDisplay;
        private ChatView _currentChat;
        private string _newFriendUsername;
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

        public string NewFriendUsername
        {
            get => _newFriendUsername;
            set => SetProperty(ref _newFriendUsername, value);
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

        private MainWindowViewModel(IAuthenticator authenticator, IDiffieHellmanKeyExchangeService keyExchangeService, IDiffieHellmanEncryptionService messageEncryptionService, IDiffieHellmanDecryptionService messageDecryptionService)
        {
            _authenticator = authenticator;
            _keyExchangeService = keyExchangeService;
            _messageEncryptionService = messageEncryptionService;
            _messageDecryptionService = messageDecryptionService;

            model = MainWindowModel.GetInstance();
            Account = new Account(_keyExchangeService, _authenticator.CurrentAccout, _messageEncryptionService, _messageDecryptionService);

            if (Account != null)
            {
                Account.MessageReceivedCallback = ReceiveMessageAsync;
                Account.FriendRequestReceivedCallback = ReceiveFriendRequestAsync;
                Account.FriendResponseReceivedCallback = ReceiveFriendResponseAsync;
                DisplayChats();
            }
        }

        public void DisplayChats() 
        {
            Chats = new List<ChatItemContainer>();
            ChatsToDisplay = new ObservableCollection<ChatView>();

            foreach (var chat in Account.Chats)
            {
                Chats.Add(ChatItemContainerFactory.Create(chat));
                ChatsToDisplay.Add(Chats[^1].ChatItem);
            }
        }

        public void CloseDialog()
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private async Task ReceiveMessageAsync(Message message)
        {
            try
            {
                var targetChat = Chats.Find(x => x.UniqueId == message.SenderUniqueId);
                message.Type = MessageType.INCOMING;

                await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() => targetChat.ViewModel.AddMessage(message)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private async Task ReceiveFriendResponseAsync(FriendResponse response)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() => RunReceiveFriendRequest(response)));
        }

        private void RunReceiveFriendRequest(FriendResponse response)
        {
            try
            {
                DisplayChats();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        private async Task ReceiveFriendRequestAsync(FriendRequest request)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                new Action(() => PerformReceiveFriendRequest(request)));
        }

        private void PerformReceiveFriendRequest(FriendRequest request)
        {
            try
            {
                FreindRequestReceivedDialogViewModel vm = new FreindRequestReceivedDialogViewModel(request, Account);
                FreindRequestReceivedDialog dialog = new FreindRequestReceivedDialog() { DataContext = vm };
                MaterialDesignThemes.Wpf.DialogHost.Show(dialog);
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

        private async Task PerformAddNewFriendAsync()
        {
            if (!string.IsNullOrEmpty(NewFriendUsername))
            {
                if (new ValidationRules.UsernameValidationRule().Validate(NewFriendUsername, null).IsValid)
                {
                    var result = await Task.Run(() => Account.SendFriendRequest(NewFriendUsername));
                    CloseDialog();
                }
            }
        }
    }
}