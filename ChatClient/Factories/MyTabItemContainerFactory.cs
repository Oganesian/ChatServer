using ChatClient.AuxiliaryClasses;
using ChatData;
using ChatClient.ViewModels;
using ChatClient.Views;

namespace ChatClient.Factories
{
    public class MyTabItemContainerFactory
    {
        public static MyTabItemContainer Create(Chat chat)
        {
            return new MyTabItemContainer 
            { 
                UniqueId = chat.receiverUniqueId, MyTabItem = new ChatTabItem { DataContext = new MyTabItemViewModel(chat) }
            };
        }
    }
}
