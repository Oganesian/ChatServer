using ChatClient.Factories;
using ChatClient.Models;
using ChatClient.Views;
using ChatData;
using System.Collections.ObjectModel;

namespace ChatClient.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private readonly ChatItemModel model;
        private ObservableCollection<IMessageUserControl> _messages;
        private string _username;

        private readonly Chat _chat;

        public ChatViewModel(Chat chat)
        {
            model = new ChatItemModel();
            Username = chat.receiverUsername + "#" + chat.receiverId;
            _chat = chat;
            DisplayChatMessages();
        }

        public int GetReceiverUniqueId()
        {
            return _chat.receiverUniqueId;
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

        private void DisplayChatMessages()
        {
            foreach (var message in _chat.messages)
            {
                Messages.Add(MessageUserControlFactory.Create(message));
            }
        }

        public void AddMessage(Message msg)
        {
            Messages.Add(MessageUserControlFactory.Create(msg));
            _chat.messages.Add(msg);
            MainWindowViewModel.GetInstance().SaveAccountChat(_chat.receiverUniqueId);
        }
    }
}
