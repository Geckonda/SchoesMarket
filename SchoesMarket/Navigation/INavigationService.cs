using Avalonia.Controls;
using SchoesMarket.ViewModels;
using ShoesMarket.Domain.Entities;
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
        void NavigateToSaveProduct();
        void NavigateToSaveProduct(ProductCardViewModel product);
        void NavigateTo<TViewModel>() where TViewModel : class;
        void ShowWindow<TWindow>() where TWindow : Window;
        void CloseCurrentWindow();
        public void CloseDialog();
    }
}
