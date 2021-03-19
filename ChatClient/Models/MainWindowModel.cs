﻿using ChatClient.AuxiliaryClasses;
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

        public Account Account { get; set; }
        //public ObservableCollection<MyTabItemContainer> Chats { get; set; }
        public ObservableCollection<MyTabItem> ChatsToDisplay { get; set; }
        public MyTabItem CurrentChat { get; set; } = null;

        public string FullUsername { get; set; }

        private MainWindowModel() 
        {
            Account = new Account(); // TODO: Remove
            ChatsToDisplay = new ObservableCollection<MyTabItem>();
        }
    }
}
