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
    // Интерфейс сервиса навигации - определяет все возможные навигационные сценарии
    public interface INavigationService
    {
        void NavigateToLogin();                          // Переход к окну логина
        void NavigateToMain();                           // Переход к главному окну
        void NavigateToSaveProduct();                    // Открытие окна создания товара
        void NavigateToSaveProduct(ProductCardViewModel product); // Открытие окна редактирования товара
        void NavigateTo<TViewModel>() where TViewModel : class; // Обобщенная навигация между ViewModel
        void ShowWindow<TWindow>() where TWindow : Window; // Показать конкретное окно
        void CloseCurrentWindow();                       // Закрыть текущее окно
        public void CloseDialog();                       // Закрыть диалоговое окно
    }

}
