using ChatClient.ClientConnection;
using ChatClient.Data;
using ChatClient.ViewModels;
using ChatClient.Views;
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
        public ObservableCollection<IMessageUserControl> Messages { get; set; }
        private MainWindowModel() 
        {
            Client = new Client();
            Client.SendMessage();
            Friends = new ObservableCollection<string>();
            Messages = new ObservableCollection<IMessageUserControl>();
        }

    }
}
