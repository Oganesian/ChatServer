using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Models
{
    public class SendMessageBoxAndButtonsModel
    {
        #region Singleton
        private static SendMessageBoxAndButtonsModel instance;
        public static SendMessageBoxAndButtonsModel GetInstance()
        {
            if (instance == null)
            {
                instance = new SendMessageBoxAndButtonsModel();
            }
            return instance;
        }

        private SendMessageBoxAndButtonsModel()
        {

        }
        #endregion

        public string TextMessage { get; set; }
    }
}

