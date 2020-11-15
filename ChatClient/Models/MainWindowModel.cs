using ChatClient.ClientConnection;
using ChatClient.ViewModels;
using System.Collections.ObjectModel;

namespace ChatClient.Models
{
    class MainWindowModel
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
        public ObservableCollection<string> Friends { get; set; }

        private MainWindowModel() 
        {
            Client = Client.GetInstance();
            Friends = new ObservableCollection<string>();

            Friends.Add("test");
            Friends.Add("test2");
        }

    }
}
