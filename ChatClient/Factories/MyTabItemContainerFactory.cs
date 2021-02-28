using ChatClient.AuxiliaryClasses;
using ChatClient.Data;
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
                UniqueId = chat.receiverUniqueId, MyTabItem = new MyTabItem { DataContext = new MyTabItemViewModel(chat) }
            };
        }
    }
}
