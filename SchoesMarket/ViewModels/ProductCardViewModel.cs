using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MsBox.Avalonia;
using SchoesMarket.Navigation;
using ShoesMarket.Domain.Abstractions;
using ShoesMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.ViewModels
{
    public partial class ProductCardViewModel : ObservableValidator
    {
        private readonly INavigationService _navigationService;
        private readonly IBaseRepository<ProductEntity> _productRepository;
        public ProductCardViewModel(INavigationService navigationService, IBaseRepository<ProductEntity> productRepository)
        {
            _navigationService = navigationService;
            _productRepository = productRepository;

            if (this.Id == 0)
                WindowTitle = "Добавление продукта";
            else
                WindowTitle = "Редактирование продукта";
        }

        [RelayCommand]
        private void OpenEditProductWindow()
        {
            _navigationService.NavigateToSaveProduct(this);
        }

        [ObservableProperty]
        private string windowTitle = "Сохранение";

        [ObservableProperty]
        private int id;

        [ObservableProperty]
        [Required(ErrorMessage = "Артикул обязателен")]
        private string article;

        [ObservableProperty]
        [Required(ErrorMessage = "Название обязательно")]
        [MinLength(2, ErrorMessage = "Название должно содержать минимум 2 символа")]
        [MaxLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        private string name;

        [ObservableProperty]
        [Required(ErrorMessage = "Единица измерения обязательна")]
        private string unitofMeasurement;


        [ObservableProperty]
        [Required(ErrorMessage = "Цена обязательна")]
        private int price;


        [ObservableProperty]
        [Required(ErrorMessage = "Поставщик обязателен")]
        private string supplier;

        [ObservableProperty]
        [Required(ErrorMessage = "Производитель обязателен")]
        private string manufacturer;


        [ObservableProperty]
        [Required(ErrorMessage = "Категория обязательна")]
        private string category;

        [ObservableProperty]
        [Required(ErrorMessage = "Количество обязательно")]
        private int amount;

        [ObservableProperty]
        [Range(0, 100, ErrorMessage = "Скидка должна быть от 0% до 100%")]
        private int discount;

        [ObservableProperty]
        [MaxLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        private string description;

        [ObservableProperty]
        private Bitmap photo;

        [ObservableProperty]
        private string photoPath;

        [RelayCommand]
        private async Task Delete()
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Ghtleght;ltybt", "Вы точно хотите удалить товар?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
            var result = await box.ShowAsync();
            if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
            {
                try
                {
                    _productRepository.Delete(this.Id);
                }
                catch (Exception ex)
                {
                    box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Ошибка удаления. ", MsBox.Avalonia.Enums.ButtonEnum.Ok);
                    result = await box.ShowAsync();
                }
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка валидации", "Проверьте поля", MsBox.Avalonia.Enums.ButtonEnum.Ok);
                var result = await box.ShowAsync();
                return;
            }

            var product = new ProductEntity()
            {
                Id = this.Id,
                Article = this.Article,
                Name = this.Name,
                UnitOfMeasurement = this.UnitofMeasurement,
                Price = this.Price,
                Supplier = this.Supplier,
                Manufacturer = this.Manufacturer,
                Category = this.Category,
                Amount = this.Amount,
                Discount = this.Discount,
                Description = this.Description,
                Photo = this.PhotoPath
            };

            try
            {
                if (product.Id == 0)
                    _productRepository.Add(product);
                else
                {
                    var existingProduct = _productRepository.GetOneById(this.Id);
                    if (existingProduct != null)
                    {
                        // Обновляем свойства существующей сущности
                        existingProduct.Article = this.Article;
                        existingProduct.Name = this.Name;
                        existingProduct.UnitOfMeasurement = this.UnitofMeasurement;
                        existingProduct.Price = this.Price;
                        existingProduct.Supplier = this.Supplier;
                        existingProduct.Manufacturer = this.Manufacturer;
                        existingProduct.Category = this.Category;
                        existingProduct.Amount = this.Amount;
                        existingProduct.Discount = this.Discount;
                        existingProduct.Description = this.Description;
                        existingProduct.Photo = this.PhotoPath;

                        _productRepository.Update(existingProduct);
                    }
                }
                    _navigationService.CloseDialog();
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Ошибка добавления в репозиторий. ", MsBox.Avalonia.Enums.ButtonEnum.Ok);
                var result = await box.ShowAsync();
            }
        }
    }
}
