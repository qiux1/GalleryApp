using System.Linq;
using Xamarin.Forms;
using GalleryApp.Models;
using System.Collections.ObjectModel;
using System;

namespace GalleryApp.Views
{
    public partial class FavoritePage : ContentPage
    {
        // Collection of favorite images to display
        public ObservableCollection<ImageModel> FavoriteImages { get; private set; }

        // Constructor: Initializes the FavoritePage with a collection of all images
        public FavoritePage(ObservableCollection<ImageModel> allImages)
        {
            InitializeComponent();

            // Filter the allImages collection to include only those marked as favorite
            FavoriteImages = new ObservableCollection<ImageModel>

                (allImages.Where(img => img.IsFavorite));

            // Set the page's BindingContext
            BindingContext = this;
        }

        // Event handler for the Gallery toolbar item click
        private async void OnGalleryClicked(object sender, EventArgs e)
        {
            // Navigate back to the root page of the navigation stack, typically the GalleryPage
            await Navigation.PopToRootAsync();
        }
    }
}
