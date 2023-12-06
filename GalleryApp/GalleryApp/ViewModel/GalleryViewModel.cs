using System.Collections.ObjectModel;
using Xamarin.Forms;
using GalleryApp.Models;
using System.Windows.Input;

namespace GalleryApp.ViewModel
{
    public class GalleryViewModel : BaseViewModel
    {
        private ObservableCollection<ImageModel> _images;

        public ObservableCollection<ImageModel> Images
        {
            get { return _images; }
            set { SetProperty(ref _images, value); }
        }

        public GalleryViewModel()
        {
            Images = new ObservableCollection<ImageModel>
            {
                new ImageModel { Filename = "Image1.jpg", IsFavorite = false },
                new ImageModel { Filename = "Image2.jpg", IsFavorite = false },
                new ImageModel { Filename = "Image3.jpg", IsFavorite = false },
                new ImageModel { Filename = "Image4.jpg", IsFavorite = false },
                // Add more images as needed
            };

            ToggleFavoriteCommand = new Command<ImageModel>(ToggleFavorite);
        }

        public ICommand ToggleFavoriteCommand { get; private set; }

        private void ToggleFavorite(ImageModel image)
        {
            if (image != null)
            {
                image.IsFavorite = !image.IsFavorite;
                OnPropertyChanged(nameof(Images)); // Notify that the Images collection has changed
            }
        }
    }
}
