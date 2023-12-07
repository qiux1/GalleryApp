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
        public ObservableCollection<ImageModel> Images { get; private set; }
        private string _currentImageFilename;
        //public ICommand NavigateToFavoritesCommand { get; private set; }
        double width = 0;
        double height = 0;

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

        public DetailPage(string currentImageFilename, ObservableCollection<ImageModel> images)
        {
            InitializeComponent();
            
            _currentImageFilename = currentImageFilename;
            Images = images;
            BindingContext = this;

            //NavigateToFavoritesCommand = new Command(async () =>
            //{
                // Create an ObservableCollection from the list of favorite images
            //   await Navigation.PushAsync(new FavoritePage(new ObservableCollection<ImageModel>(Images.Where(img => img.IsFavorite))));
            //});
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
                    AdjustCarouselViewMargin(new Thickness(20, 10));
                }
                else
                {
                    // Portrait layout adjustments
                    AdjustCarouselViewMargin(new Thickness(10));
                }
            }
        }

        private void AdjustCarouselViewMargin(Thickness margin)
        {
            if (this.FindByName<CarouselView>("imageCarousel") is CarouselView carouselView)
            {
                carouselView.Margin = margin;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateCurrentItem();
        }

        private void UpdateCurrentItem()
        {
            var currentIndex = Images.IndexOf(Images.FirstOrDefault(img => img.Filename == _currentImageFilename));
            if (currentIndex >= 0)
            {
                var carouselView = this.FindByName<CarouselView>("imageCarousel");
                carouselView.Position = currentIndex;
            }
        }

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