using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.Converters
{
    public class StringToBitmapConverter
    {
        public static Bitmap? LoadProductImage(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return LoadDefaultImage();

            try
            {
                // Убираем возможные лишние символы
                fileName = fileName.Trim();

                // Формируем URI к ресурсу
                var uri = new Uri($"avares://SchoesMarket/Assets/{fileName}");
                return new Bitmap(AssetLoader.Open(uri));
            }
            catch
            {
                return LoadDefaultImage();
            }
        }

        public static Bitmap? LoadDefaultImage()
        {
            try
            {
                return new Bitmap(AssetLoader.Open(new Uri("avares://SchoesMarket/Assets/picture.png")));
            }
            catch
            {
                return null;
            }
        }
    }
}
