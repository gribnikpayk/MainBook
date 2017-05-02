using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MainBook.Infrastructure.CommonData;
using Xamarin.Forms;

namespace MainBook.ViewModels
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Color _backgroundColor;
        public BaseViewModel()
        {
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public INavigation Navigation { get; set; }

        public Color BackgroundColor
        {
            set { SetProperty(ref _backgroundColor, value); }
            get { return CommonData.IsNightMode ? Color.FromHex("#15161a") : Color.White; }
        }
    }
}
