using ChatClient.ClientConnection;
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

        public Client Client { get; set; }
        public ObservableCollection<MyTabItem> Friends { get; set; }
        private MainWindowModel() 
        {
            Client = new Client();
            Friends = new ObservableCollection<MyTabItem>();
        }
    }
}
