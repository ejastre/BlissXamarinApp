using System.ComponentModel;
using BlissXamarinApp.Utils;
using BlissXamarinApp.ViewModels;
using Xamarin.Forms;

namespace BlissXamarinApp.Views
{
    public abstract class BasePage : ContentPage
    {
        private BaseViewModel ViewModel => BindingContext as BaseViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!Util.CheckConnectivity())
            {
                await Navigation.PushAsync(new OfflinePage());
                return;
            }
            if (ViewModel == null) return;

            Title = ViewModel.Title;
            ViewModel.PropertyChanged += TitlePropertyChanged;
            await ViewModel.LoadAsync();
        }

        private void TitlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.Title)) return;

            Title = ViewModel.Title;
        }
    }
}
