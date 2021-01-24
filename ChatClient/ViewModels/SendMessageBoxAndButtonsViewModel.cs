using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.Models;
using System;
using System.Linq;
using System.Windows.Input;

namespace ChatClient.ViewModels
{
    public class SendMessageBoxAndButtonsViewModel : BindableBase
    {
        private readonly SendMessageBoxAndButtonsModel model;

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
                    ReceiverUniqueId = 0,
                    Timestamp = DateTime.Now,
                    EncryptedMessageString = TextMessage,
                    Type = MessageType.OUTGOING,
                    Status = MessageStatus.SENDED
                };

                sender.SendMessage(message);
                TextMessage = string.Empty;

                // if(sender.SendMessage(message)) { ...
                var targetChat = MainWindowViewModel.GetInstance().Chats.Where(x=> x.UniqueId == message.ReceiverUniqueId);
                targetChat.ElementAt(0).ViewModel.Messages.Add(Factories.MessageUserControlFactory.Create(message));
            }
        }
    }
}
