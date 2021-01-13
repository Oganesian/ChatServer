using ChatClient.Data;
using ChatClient.ViewModels;
using ChatClient.Views;

namespace ChatClient.Factories
{
    public class MyTabItemFactory
    {
        public static MyTabItem Create(Chat chat)
        {
            return new MyTabItem { DataContext = new MyTabItemViewModel(chat) };
        }
    }
}
