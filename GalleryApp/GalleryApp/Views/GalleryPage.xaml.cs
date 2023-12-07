using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalleryApp.Models;
using GalleryApp.ViewModel;
using Xamarin.Forms;

namespace GalleryApp.Views
{
    public partial class GalleryPage : ContentPage
    {
        readonly GalleryViewModel viewModel;
        // Initialize the Images collection
        public ObservableCollection<ImageModel> Images { get; private set; }
        double width = 0;
        double height = 0;

        public GalleryPage()
        {
            InitializeComponent();
            viewModel = new GalleryViewModel();
            BindingContext = viewModel;

            Images = viewModel.Images;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                if (width > height)
                {
                    // Landscape layout adjustments
                    AdjustCollectionViewLayout(4); // Example: 4 columns in landscape
                }
                else
                {
                    // Portrait layout adjustments
                    AdjustCollectionViewLayout(3); // Example: 3 columns in portrait
                }
            }
        }

        private void AdjustCollectionViewLayout(int columns)
        {
            if (galleryCollectionView?.ItemsLayout is GridItemsLayout gridLayout)
            {
                gridLayout.Span = columns;
            }
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as ImageModel;
            if (currentSelection == null)
                return;

            // Navigate to the detail page with the selected image and the images collection
            await Navigation.PushAsync(new DetailPage(currentSelection.Filename, viewModel.Images));

            // Deselect the item
            ((CollectionView)sender).SelectedItem = null;
        }

        // New event handler for Favorites toolbar item clicked
        private async void OnFavoritesClicked(object sender, EventArgs e)
        {
            // Assuming FavoritePage has a constructor that accepts a collection of ImageModel
            var favoriteImages = new ObservableCollection<ImageModel>(Images.Where(img => img.IsFavorite));
            var favoritePage = new FavoritePage(favoriteImages);
            await Navigation.PushAsync(favoritePage);
        }
    }
}