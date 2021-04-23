using ChatClient.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ChatClient.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window, ICloseable
    {
        public RegisterWindow(RegisterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void LoginWindowInstance_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseWindowCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void MinimizeWindowCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
    }
}
