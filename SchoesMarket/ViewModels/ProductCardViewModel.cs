using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.ViewModels
{
    public partial class ProductCardViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;


        [ObservableProperty]
        private string article;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string unitofMeasurement;


        [ObservableProperty]
        private int price;


        [ObservableProperty]
        private string supplier;

        [ObservableProperty]
        private string manufacturer;


        [ObservableProperty]
        private string category;

        [ObservableProperty]
        private int amount;

        [ObservableProperty]
        private int discount;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private Bitmap photo;
    }
}
