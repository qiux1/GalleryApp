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
        public ICommand NavigateBackCommand { get; private set; }

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
            NavigateBackCommand = new Command(async () => await Navigation.PopAsync());
            _currentImageFilename = currentImageFilename;
            Images = images;
            BindingContext = this;
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
                // Optionally, update the UI or save the favorite state
            }
        }
    }
}