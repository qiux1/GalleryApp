using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GalleryApp.Models
{
    public class ImageModel : INotifyPropertyChanged
    {
        private string _filename;
        private bool _isFavorite;

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
