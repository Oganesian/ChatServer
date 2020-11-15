using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatClient.ClientConnection;
using ChatClient.Models;

namespace ChatClient.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private MainWindowModel model;

        #region Model reference types
        private Client _client;
        private ObservableCollection<string> _friends;
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
        #endregion

        #region Commands
        private ICommand _connectToServer;
        private ICommand _sendMessage;
        #endregion

        public MainWindowViewModel()
        {
            model = MainWindowModel.GetInstance();
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