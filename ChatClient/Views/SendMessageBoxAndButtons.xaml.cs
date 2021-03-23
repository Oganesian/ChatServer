using ChatClient.ViewModels;
using System.Windows.Controls;

namespace ChatClient.Views
{
    /// <summary>
    /// Interaction logic for SendMessageBoxAndButtons.xaml
    /// </summary>
    public partial class SendMessageBoxAndButtons : UserControl
    {
        public SendMessageBoxAndButtons()
        {
            InitializeComponent();
            DataContext = new SendMessageBoxAndButtonsViewModel();
        }
    }
}
