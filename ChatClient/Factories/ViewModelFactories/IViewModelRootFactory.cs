using ChatClient.ViewModels;

namespace ChatClient.Factories.ViewModelFactories
{
    public interface IViewModelRootFactory
    {
        public BindableBase CreateViewModel(ViewType type);
    }
}

namespace ChatClient.Factories
{
    public enum ViewType
    {
        MainWindow,
        LoginWindow,
        RegisterWindow
    }
}