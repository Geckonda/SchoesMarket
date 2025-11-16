using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoesMarket.Converters;
using SchoesMarket.Navigation;
using ShoesMarket.Domain.Abstractions;
using ShoesMarket.Domain.Entities;
using ShoesMarket.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            List<ProductEntity> list = [.. _productRepository.GetAll() ?? new()];

            foreach (var item in list)
            {
                // Формирование пути до картинки
                var photoPath = new Uri("avares://SchoesMarket/Assets/" + (item.Photo ?? "picture.png")); // В случае, если item.photo == null выведем default
                Products.Add(new ProductCardViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Manufacturer = item.Manufacturer,
                    Supplier = item.Supplier,
                    Price = item.Price,
                    UnitofMeasurement = item.UnitOfMeasurement,
                    Amount = item.Amount,
                    Discount = item.Discount,
                    // Создаем объект картинки (BitMap)
                    Photo = new Bitmap(AssetLoader.Open(photoPath)),
                });
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

        }
    }
}
