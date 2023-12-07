using System.Linq;
using Xamarin.Forms;
using GalleryApp.Models;
using System.Collections.ObjectModel;
using System;

namespace GalleryApp.Views
{
    public partial class FavoritePage : ContentPage
    {
        public ObservableCollection<ImageModel> FavoriteImages { get; private set; }

        public FavoritePage(ObservableCollection<ImageModel> allImages)
        {
            InitializeComponent();
            FavoriteImages = new ObservableCollection<ImageModel>(allImages.Where(img => img.IsFavorite));
            BindingContext = this;
        }

        // New event handler for Gallery toolbar item clicked
        private async void OnGalleryClicked(object sender, EventArgs e)
        {
            // Navigate back to the GalleryPage
            await Navigation.PopToRootAsync();
        }
    }
}
