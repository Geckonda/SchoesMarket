using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoesMarket.Converters;
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

        [ObservableProperty]
        private ObservableCollection<ProductCardViewModel> _products = new();
        public MainWindowViewModel(IBaseRepository<ProductEntity> productRepository)
        {
            _productRepository = productRepository;

            Refresh();
        }

        [RelayCommand]
        private void Refresh()
        {
            List<ProductEntity> list = [.. _productRepository.GetAll() ?? new()];

            foreach (var item in list)
            {
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
                    Photo = StringToBitmapConverter.LoadProductImage(item.Photo)!
                });
            }
        }



        [RelayCommand]
        private void OpenAddProductWindow()
        {

        }
    }
}
