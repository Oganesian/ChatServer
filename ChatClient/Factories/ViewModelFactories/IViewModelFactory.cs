using ChatClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatClient.Factories.ViewModelFactories
{
    public interface IViewModelFactory<T> where T : BindableBase
    {
        T CreateViewModel();
    }
}