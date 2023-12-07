using System;
using System.Globalization;
using Xamarin.Forms;

namespace GalleryApp.Converters
{
    // This converter is used in XAML to convert a boolean value to a specific image source.
    // If the item is marked as favorite, it returns a filled heart image.
    // If the item is not a favorite, it returns an outlined heart image.
    public class FavoriteIconConverter : IValueConverter
    {
        // The Convert method is called when data flows from the source to the target.
        // 'value' is the data from the source, which in this case is a boolean indicating the favorite status.
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "favorite_filled.png" : "favorite_outline.png";
        }

        // The ConvertBack method is called when data flows from the target back to the source,
        // which is not implemented in this converter as the conversion is one way.
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
