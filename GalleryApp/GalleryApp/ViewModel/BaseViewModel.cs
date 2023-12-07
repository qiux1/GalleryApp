using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GalleryApp.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Event to notify the UI of property changes.
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged method used to trigger the PropertyChanged event.
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // SetProperty is a utility method to set a property and notify the UI of changes.
        // It returns true if the value has changed, false otherwise.
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            System.Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}