using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GalleryApp.Models
{
    public class ImageModel : INotifyPropertyChanged
    {
        private string _filename;
        private bool _isFavorite;

        // Event triggered when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged;

        // Filename property with a getter and a setter.
        // When set, it triggers the OnPropertyChanged method to notify any subscribers.
        public string Filename
        {
            get => _filename;
            set
            {
                if (_filename != value)
                {
                    _filename = value;
                    OnPropertyChanged();
                }
            }
        }

        // IsFavorite property indicating whether the image is marked as favorite.
        // Similar to Filename, it notifies of changes.
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged();
                }
            }
        }

        // OnPropertyChanged method used to trigger the PropertyChanged event.
        // CallerMemberName attribute automatically gets the name of the caller property.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
