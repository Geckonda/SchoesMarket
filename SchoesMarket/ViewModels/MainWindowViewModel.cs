using Avalonia.Media.Imaging;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoesMarket.Converters;
using SchoesMarket.Navigation;
using ShoesMarket.Domain.Abstractions;
using ShoesMarket.Domain.Entities;
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
        private ObservableCollection<ProductCardViewModel> _products = new();
        public MainWindowViewModel(IBaseRepository<ProductEntity> productRepository,
            INavigationService navigationService)
        {
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
                var photoPath = new Uri("avares://SchoesMarket/Assets/" + (item.Photo ?? "picture.png"));
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
                    Photo = new Bitmap(AssetLoader.Open(photoPath)),
                });
            }
        }

        [RelayCommand]
        private void OpenAuth()
        {
            _navigationService.NavigateToLogin();
        }

        [RelayCommand]
        private void OpenAddProductWindow()
        {

        }
    }
}
