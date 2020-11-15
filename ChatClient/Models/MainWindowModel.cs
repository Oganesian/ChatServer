using ChatClient.ViewModels;

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

        private MainWindowModel() { }
        
        public bool HamburgerMenuOpened { get; set; } // TODO: delete

    }
}
