using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.Views;

namespace ChatClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly MainWindowModel model;

        #region Model reference types
        private Client _client;
        private ObservableCollection<string> _friends;
        private ObservableCollection<IMessageUserControl> _messages;
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

        public ObservableCollection<string> Friends
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
        #endregion

        #region Commands
        private ICommand _connectToServer;
        private ICommand _sendMessage;
        #endregion

        public MainWindowViewModel()
        {
            model = MainWindowModel.GetInstance();

            Messages.Add(MessageUserControlFactory.Create(MessageType.INCOMING, "hello world"));
            Messages.Add(MessageUserControlFactory.Create(MessageType.OUTGOING, "hey mate"));
            Messages.Add(MessageUserControlFactory.Create(MessageType.INCOMING, "what's up?"));
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

        public ICommand SendMessage
        {
            get
            {
                if (_sendMessage == null)
                {
                    _sendMessage = new CommandHandler(() => SendMessageExec(), () => CanExecute);
                }
                return _sendMessage;
            }
        }
        
        #endregion

        private void ConectToServerExec()
        {

        }

        private void SendMessageExec()
        {
            Client.SendMessage();
        }

    }
}