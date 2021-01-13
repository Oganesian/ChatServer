using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Models;
using System;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class SendMessageBoxAndButtonsViewModel : BindableBase
    {
        private SendMessageBoxAndButtonsModel model;

        //private Client receiver;
        private Client sender;

        private ICommand _sendMessage;

        private string _textMessage;

        public SendMessageBoxAndButtonsViewModel()
        {
            model = SendMessageBoxAndButtonsModel.GetInstance();
        }

        public string TextMessage
        {
            get
            {
                return model.TextMessage;
            }
            set
            {
                model.TextMessage = value;
                SetProperty(ref _textMessage, value);
            }
        }

        #region Commands Getters
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

        private void SendMessageExec()
        {
            if (!string.IsNullOrEmpty(TextMessage))
            {
                sender = new Client(); // TODO: Remove this
                var message = new Message()
                {
                    Timestamp = DateTime.Now,
                    EncryptedMessageString = TextMessage
                };

                sender.SendMessage(message);
                TextMessage = string.Empty;
            }
        }
    }
}
