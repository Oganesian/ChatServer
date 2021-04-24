using ChatClient.AuxiliaryClasses;
using ChatData;
using ChatClient.ViewModels;
using ChatClient.Views;

namespace ChatClient.Factories
{
    public class ChatItemContainerFactory
    {
        public static ChatItemContainer Create(Chat chat)
        {
            return new ChatItemContainer 
            { 
                UniqueId = chat.receiverUniqueId, ChatItem = new ChatView { DataContext = new ChatViewModel(chat) }
            };
        }
    }
}
