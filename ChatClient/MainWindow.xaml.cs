using ChatClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(BindableBase viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
