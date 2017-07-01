using System;
using BlissXamarinApp.Interfaces;
using Xamarin.Forms;

namespace BlissXamarinApp.Views
{
    public partial class OfflinePage : ContentPage
    {
        public OfflinePage()
        {
            InitializeComponent();
        }

        private async void RefreshOnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Close", "Ops! Do you want close the App?", "YES", "NO"))
                DependencyService.Get<IAndroidMethods>().CloseApp();
        }
    }
}
