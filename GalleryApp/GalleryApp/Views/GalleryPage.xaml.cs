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
        public ObservableCollection<ImageModel> Images { get; private set; }
        double width = 0;
        double height = 0;

        // Constructor
        public GalleryPage()
        {
            InitializeComponent();
            viewModel = new GalleryViewModel();
            BindingContext = viewModel;
            Images = viewModel.Images;
        }

        // Called when the size of the page is allocated (useful for handling orientation changes)
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Check if there's a change in the layout's width or height
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;

                // Adjust the layout based on the new width and height
                if (width > height)
                {
                    // Landscape mode: adjust the layout accordingly
                    AdjustCollectionViewLayout(4);
                }
                else
                {
                    // Portrait mode: adjust the layout accordingly
                    AdjustCollectionViewLayout(3);
                }
            }
        }

        // Adjusts the number of columns in the CollectionView based on the orientation
        private void AdjustCollectionViewLayout(int columns)
        {
            if (galleryCollectionView?.ItemsLayout is GridItemsLayout gridLayout)
            {
                gridLayout.Span = columns;
            }
        }

        // Event handler for when an item in the CollectionView is selected
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as ImageModel;
            if (currentSelection == null)
                return;

            // Navigate to the DetailPage for the selected image
            await Navigation.PushAsync(new DetailPage(currentSelection.Filename, viewModel.Images));
            ((CollectionView)sender).SelectedItem = null; // Deselect the item
        }

        // Event handler for the 'Favorites' toolbar item click
        private async void OnFavoritesClicked(object sender, EventArgs e)
        {
            var favoriteImages = new ObservableCollection<ImageModel>(Images.Where(img => img.IsFavorite));
            // Navigate to the FavoritePage with the favorite images
            await Navigation.PushAsync(new FavoritePage(favoriteImages));
        }
    }
}
