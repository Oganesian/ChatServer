using ChatClient.Factories.ViewModelFactories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ChatClient.Factories.WindowFactories
{
    public interface IWindowFactory
    {
        Window CreateWindow(ViewType type);
    }
}
