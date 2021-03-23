using ChatClient.ViewModels;
using System.Windows;

namespace ChatClient.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, ICloseable
    {
        public LoginWindow(BindableBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
