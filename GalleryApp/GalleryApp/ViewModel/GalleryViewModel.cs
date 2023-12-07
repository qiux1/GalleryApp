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
            Images = new ObservableCollection<ImageModel>();

            
            int totalImages = 25;
            for (int i = 1; i <= totalImages; i++)
            {
                string filename = $"Image{i}.jpg";
                Images.Add(new ImageModel { Filename = filename, IsFavorite = false });
            }

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
