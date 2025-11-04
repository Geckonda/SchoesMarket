using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.Navigation
{
    public interface INavigationService
    {
        void NavigateToLogin();
        void NavigateToMain();
        void NavigateTo<TViewModel>() where TViewModel : class;
        void ShowWindow<TWindow>() where TWindow : Window;
        void CloseCurrentWindow();
    }
}
