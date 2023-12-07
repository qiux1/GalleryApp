using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using GalleryApp.Models;
using System;
using System.Windows.Input;

namespace GalleryApp.Views
{
    public partial class DetailPage : ContentPage
    {
        // Holds the collection of images passed to this page.
        public ObservableCollection<ImageModel> Images { get; private set; }
        // Stores the filename of the currently displayed image.
        private string _currentImageFilename;
        double width = 0;
        double height = 0;

        // Property for getting and setting the current image filename.
        // Triggers UI updates when the current image changes.
        public string CurrentImageFilename
        {
            get => _currentImageFilename;
            set
            {
                if (_currentImageFilename != value)
                {
                    _currentImageFilename = value;
                    OnPropertyChanged(nameof(CurrentImageFilename));
                    UpdateCurrentItem();
                }
            }
        }

        // Constructor
        public DetailPage(string currentImageFilename, ObservableCollection<ImageModel> images)
        {
            InitializeComponent();
            _currentImageFilename = currentImageFilename;
            Images = images;
            BindingContext = this;
        }

        // Handles responsive layout changes, such as orientation change.
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
                    AdjustCarouselViewMargin(new Thickness(20, 10));
                }
                else
                {
                    // Portrait layout adjustments
                    AdjustCarouselViewMargin(new Thickness(10));
                }
            }
        }

        // Adjusts the margin around the CarouselView based on orientation.
        private void AdjustCarouselViewMargin(Thickness margin)
        {
            if (this.FindByName<CarouselView>
                ("imageCarousel") is CarouselView carouselView)
            {
                carouselView.Margin = margin;
            }
        }

        // Ensures the UI is updated with the correct image when the page appears.
        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateCurrentItem();
        }

        // Updates the position of the CarouselView to display the current image.
        private void UpdateCurrentItem()
        {
            var currentIndex = Images.IndexOf(
                Images.FirstOrDefault(img => img.Filename == _currentImageFilename));
            if (currentIndex >= 0)
            {
                var carouselView = this.FindByName<CarouselView>("imageCarousel");
                carouselView.Position = currentIndex;
            }
        }

        // Event handlers for navigating between images and toggling favorite status.
        private void OnPositionChanged(object sender, PositionChangedEventArgs e)
        {
            if (e.CurrentPosition >= 0 && e.CurrentPosition < Images.Count)
            {
                CurrentImageFilename = Images[e.CurrentPosition].Filename;
            }
        }

        private void OnPreviousClicked(object sender, EventArgs e)
        {
            var currentIndex = imageCarousel.Position;
            if (currentIndex > 0)
                imageCarousel.Position = currentIndex - 1;
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            var currentIndex = imageCarousel.Position;
            if (currentIndex < Images.Count - 1)
                imageCarousel.Position = currentIndex + 1;
        }

        private void OnFavoriteTapped(object sender, EventArgs e)
        {
            if (sender is Image image && image.BindingContext is ImageModel model)
            {
                model.IsFavorite = !model.IsFavorite;
        
            }
        }

        // Event handler for Favorites toolbar item clicked
        private async void OnFavoritesClicked(object sender, EventArgs e)
        {
            var favoriteImages = new ObservableCollection<ImageModel>(Images.Where(img => img.IsFavorite));
            var favoritePage = new FavoritePage(favoriteImages);
            await Navigation.PushAsync(favoritePage);
        }
    }
}