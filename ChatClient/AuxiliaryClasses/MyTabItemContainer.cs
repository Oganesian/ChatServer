using ChatClient.ViewModels;
using ChatClient.Views;

namespace ChatClient.AuxiliaryClasses
{
    public class MyTabItemContainer
    {
        public int UniqueId { get; set; }
        public MyTabItemViewModel ViewModel
        {
            get
            {
                return MyTabItem?.DataContext as MyTabItemViewModel;
            }
            set
            {
                if(MyTabItem != null) MyTabItem.DataContext = value; // MyTabItem?.DataContext = value; doesn't work for some reason
            }
        }
        public ChatTabItem MyTabItem { get; set; }
    }
}
