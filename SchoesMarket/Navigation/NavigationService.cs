using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using SchoesMarket.ViewModels;
using SchoesMarket.Views;
using ShoesMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IClassicDesktopStyleApplicationLifetime _desktop;
        private readonly IServiceProvider _services;
        private Window _currentWindow;

        public NavigationService(IClassicDesktopStyleApplicationLifetime desktop, IServiceProvider services)
        {
            _desktop = desktop;
            _services = services;
        }

        public void NavigateToLogin()
        {
            var loginWindow = _services.GetRequiredService<LoginWindow>();
            loginWindow.DataContext = _services.GetRequiredService<LoginViewModel>();

            // Подписываемся на событие успешного логина
            if (loginWindow.DataContext is LoginViewModel loginViewModel)
            {
                loginViewModel.RequestNavigateToMain += NavigateToMain;
            }

            ShowWindow(loginWindow);
        }
        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            // Для навигации между View внутри одного окна
            if (_currentWindow?.DataContext is INavigationAware navigationAware)
            {
                var viewModel = _services.GetRequiredService<TViewModel>();
                navigationAware.NavigateTo(viewModel);
            }
        }
        public void NavigateToMain()
        {
            var mainWindow = _services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _services.GetRequiredService<MainWindowViewModel>();
            ShowWindow(mainWindow);
        }
        public void NavigateToSaveProduct()
        {
            var saveProductWindow = _services.GetRequiredService<SaveProductWindow>();
            saveProductWindow.DataContext = _services.GetRequiredService<ProductCardViewModel>();
            ShowDialogWindow(saveProductWindow);
        }
        public void ShowWindow<TWindow>() where TWindow : Window
        {
            var window = _services.GetRequiredService<TWindow>();
            ShowWindow(window);
        }

        public void CloseCurrentWindow()
        {
            _currentWindow?.Close();
        }

        private void ShowWindow(Window window)
        {
            _desktop.MainWindow = window;
            window.Show();
            if(_currentWindow!= null) _currentWindow.Close();
            _currentWindow = window;
        }
        private void ShowDialogWindow(Window window)
        {
            window.ShowDialog(_currentWindow);
        }

        public void NavigateToSaveProduct(ProductCardViewModel product)
        {
            var saveProductWindow = _services.GetRequiredService<SaveProductWindow>();
            saveProductWindow.DataContext = product;
            ShowDialogWindow(saveProductWindow);
        }
        public void CloseDialog()
        {
            // Закрываем все диалоговые окна
            if (_currentWindow != null)
            {
                foreach (Window window in _desktop.Windows)
                {
                    if (window != _currentWindow && window is SaveProductWindow)
                    {
                        window.Close();
                        break;
                    }
                }
            }
        }
    }
}
