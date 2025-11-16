using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SchoesMarket.Navigation;
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
        public ProductCardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        private void OpenEditProductWindow()
        {
            _navigationService.NavigateToSaveProduct(this);
        }

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

        [RelayCommand]
        private void Save()
        {
            ValidateAllProperties();

            if (HasErrors)
            {
                Debug.WriteLine("НАРУШЕНИЕ");
                return;
            }

            Debug.WriteLine("Все хорошо");
            // Логика сохранения
        }
    }
}
