using ChatClient.ViewModels;
using ChatClient.Views;

namespace ChatClient.AuxiliaryClasses
{
    public class ChatItemContainer
    {
        public int UniqueId { get; set; }
        public ChatView ChatItem { get; set; }


        public ChatViewModel ViewModel
        {
            get
            {
                return ChatItem?.DataContext as ChatViewModel;
            }
            set
            {
                if(ChatItem != null) ChatItem.DataContext = value; // MyTabItem?.DataContext = value; doesn't work for some reason
            }
        }
    }
}
