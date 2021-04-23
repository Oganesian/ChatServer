using AccountAndConnection;
using ChatClient.AuxiliaryClasses;
using ChatClient.Views;
using System.Collections.ObjectModel;

namespace ChatClient.Models
{
    public class MainWindowModel
    {
        #region Singleton
        private static MainWindowModel instance;
        public static MainWindowModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainWindowModel();
            }
            return instance;
        }
        #endregion

        public Account Account { get; set; }
        //public ObservableCollection<MyTabItemContainer> Chats { get; set; }
        public ObservableCollection<ChatTabItem> ChatsToDisplay { get; set; }
        public ChatTabItem CurrentChat { get; set; } = null;

        public string FullUsername { get; set; }

        private MainWindowModel() 
        {
            ChatsToDisplay = new ObservableCollection<ChatTabItem>();
        }
    }
}
