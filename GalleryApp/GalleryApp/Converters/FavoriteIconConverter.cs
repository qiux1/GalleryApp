using System;
using System.Globalization;
using Xamarin.Forms;

namespace GalleryApp.Converters
{
    public class FavoriteIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Assume you have "favorite_filled.png" and "favorite_outline.png" as your icon images
            return (bool)value ? "favorite_filled.png" : "favorite_outline.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
