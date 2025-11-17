using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using MsBox.Avalonia;
using SchoesMarket.Converters;
using SchoesMarket.Navigation;
using ShoesMarket.Domain.Abstractions;
using ShoesMarket.Domain.Entities;
using ShoesMarket.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoesMarket.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {

        private readonly IBaseRepository<ProductEntity> _productRepository;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private UserRole currentUserRole;

        // Свойства видимости для разных ролей
        // Админ видит всё
        public bool IsAdmin => CurrentUserRole == UserRole.Admin;

        // Менеджер и выше
        public bool IsManager => CurrentUserRole == UserRole.Manager || IsAdmin;

        // Все пользователи (включая админов и менеджеров)
        public bool IsUser => CurrentUserRole == UserRole.User || IsManager;

        [ObservableProperty]
        private ObservableCollection<ProductCardViewModel> _products = new();
        public MainWindowViewModel(IBaseRepository<ProductEntity> productRepository,
            INavigationService navigationService)
        {
            // Получение роли пользователя
            CurrentUserRole = App.CurrentUserRole;
            _productRepository = productRepository;
            _navigationService = navigationService;
            Refresh();
        }

        [RelayCommand]
        private void Refresh()
        {
            Products = new();
            List<ProductEntity> list = [.. _productRepository.GetAll() ?? new()];
            list = list.OrderByDescending(x => x.Id).ToList();
            foreach (var item in list)
            {
                // Формирование пути до картинки
                var photoPath = new Uri("avares://SchoesMarket/Assets/" + (item.Photo ?? "picture.png")); // В случае, если item.photo == null выведем default
                var vm = App.Services.GetRequiredService<ProductCardViewModel>();

                // Формируем нашу ViewModel 

                vm.Id = item.Id;
                vm.Article = item.Article;
                vm.Category = item.Category;
                vm.Name = item.Name;
                vm.Description = item.Description;
                vm.Manufacturer = item.Manufacturer;
                vm.Supplier = item.Supplier;
                vm.Price = item.Price;
                vm.UnitofMeasurement = item.UnitOfMeasurement;
                vm.Amount = item.Amount;
                vm.Discount = item.Discount;
                // Создаем объект картинки (BitMap)
                vm.Photo = new Bitmap(AssetLoader.Open(photoPath));
                vm.PhotoPath = item.Photo;
                // Добавляем в коллекцию
                Products.Add(vm);
            }
        }

        [RelayCommand]
        private void OpenAuth()
        {
            // Открывает окно авторизации
            _navigationService.NavigateToLogin();
        }
        [RelayCommand]
        private void OpenAddProductWindow()
        {
            // Открывает окно добавления продукта
            _navigationService.NavigateToSaveProduct();
        }

        // Метод демонстрации модульного окна с кнопкой ОК
        [RelayCommand]
        private async Task OpenOrderWindow()
        {
            try
            {
                throw new Exception("Ошибка для ОК");
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.Ok);
                var result = await box.ShowAsync();
            }
        }

        // Метод демонстрации модульного окна с кнопками ДА и НЕТ
        [RelayCommand]
        private async Task OpenNewOrderWindow()
        {
            try
            {
                throw new Exception("Ошибка для ДА и НЕТ");
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", ex.Message, MsBox.Avalonia.Enums.ButtonEnum.YesNo);
                var result = await box.ShowAsync();
                if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
                    Debug.WriteLine("YESSSSSSS");
                if(result == MsBox.Avalonia.Enums.ButtonResult.No)
                    Debug.WriteLine("NOOOOO");
            }
        }
    }
}
