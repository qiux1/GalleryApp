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

        public GalleryPage()
        {
            InitializeComponent();
            viewModel = new GalleryViewModel();
            BindingContext = viewModel;
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
    }
}