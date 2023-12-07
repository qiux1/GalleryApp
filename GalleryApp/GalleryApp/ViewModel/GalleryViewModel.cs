using System.Collections.ObjectModel;
using Xamarin.Forms;
using GalleryApp.Models;
using System.Windows.Input;

namespace GalleryApp.ViewModel
{
    public class GalleryViewModel : BaseViewModel
    {
        private ObservableCollection<ImageModel> _images;

        // Images collection that the UI will bind to.
        public ObservableCollection<ImageModel> Images
        {
            get { return _images; }
            set { SetProperty(ref _images, value); }
        }

        // Constructor 
        public GalleryViewModel()
        {
            Images = new ObservableCollection<ImageModel>();

            // Populate the Images collection with ImageModels.
            int totalImages = 25;
            for (int i = 1; i <= totalImages; i++)
            {
                string filename = $"Image{i}.jpg";
                Images.Add(new ImageModel { Filename = filename, IsFavorite = false });
            }

            // Command that invokes the ToggleFavorite method when executed.
            ToggleFavoriteCommand = new Command<ImageModel>(ToggleFavorite);
        }

        // ICommand exposed to the UI for toggling the favorite status of an image.
        public ICommand ToggleFavoriteCommand { get; private set; }

        // ToggleFavorite method changes the IsFavorite property of an ImageModel.
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