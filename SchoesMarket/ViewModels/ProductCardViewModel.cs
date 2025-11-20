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

        }

        // Открывает окно редактирвоания продукта
        [RelayCommand]
        private void OpenEditProductWindow()
        {
            WindowTitle = "Редактирование продукта";
            _navigationService.NavigateToSaveProduct(this);
        }

        // Поле заголовка окна
        [ObservableProperty]
        private string windowTitle = "Сохранение";

        [ObservableProperty]
        private int _id;

        // Следующие поля носят атрибуты валидации

        [ObservableProperty]
        [Required(ErrorMessage = "Артикул обязателен")]
        private string _article;

        [ObservableProperty]
        [Required(ErrorMessage = "Название обязательно")]
        [MinLength(2, ErrorMessage = "Название должно содержать минимум 2 символа")]
        [MaxLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        private string _name;

        [ObservableProperty]
        [Required(ErrorMessage = "Единица измерения обязательна")]
        private string _unitofMeasurement;


        [ObservableProperty]
        [Required(ErrorMessage = "Цена обязательна")]
        private int _price;


        [ObservableProperty]
        [Required(ErrorMessage = "Поставщик обязателен")]
        private string _supplier;

        [ObservableProperty]
        [Required(ErrorMessage = "Производитель обязателен")]
        private string _manufacturer;


        [ObservableProperty]
        [Required(ErrorMessage = "Категория обязательна")]
        private string _category;

        [ObservableProperty]
        [Required(ErrorMessage = "Количество обязательно")]
        private int _amount;

        [ObservableProperty]
        [Range(0, 100, ErrorMessage = "Скидка должна быть от 0% до 100%")]
        private int _discount;

        [ObservableProperty]
        [MaxLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        private string _description;

        // Путь к фото
        [ObservableProperty]
        private Bitmap _photo;

        // Путь к картинке
        [ObservableProperty]
        private string _photoPath;

        [ObservableProperty]
        private bool _hasDiscount;

        [ObservableProperty]
        private int _realPrice;

        /// <summary>
        /// Удаление товара 
        /// </summary>
        [RelayCommand]
        private async Task Delete()
        {
            // Вывод подтверждение для пользователя, что он действительно уверен в удалении товара 
            var box = MessageBoxManager.GetMessageBoxStandard("Подтверждение действия", "Вы точно хотите удалить товар?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
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

        /// <summary>
        /// Метод добавления/редактирования товара
        /// </summary>
        [RelayCommand]
        private async Task Save()
        {
            // Метод объекта ObservableValidator, от которого наследуется наш класс, проверяет ошибки валидации
            ValidateAllProperties();

            // Если есть ошибки, выводим окно с ошибкой и выходим из метода созхранения
            if (HasErrors)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Ошибка валидации", "Проверьте поля", MsBox.Avalonia.Enums.ButtonEnum.Ok);
                var result = await box.ShowAsync();
                return;
            }

            // Переносим значения полей из ViewModel в Entity
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
                // Закрываем окно
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
