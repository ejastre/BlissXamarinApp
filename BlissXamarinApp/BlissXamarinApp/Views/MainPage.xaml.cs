using System;
using BlissXamarinApp.Interfaces;
using BlissXamarinApp.ViewModels;
using Xamarin.Forms;

namespace BlissXamarinApp.Views
{
    public partial class MainPage
    {
        private MainViewModel ViewModel => BindingContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
            var blissXamarinApiService = DependencyService.Get<IBlissApiService>();
            BindingContext = new MainViewModel(blissXamarinApiService);
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args == null) return; // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row

            if (args.SelectedItem != null)
            {
                ViewModel.ShowQuestionCommand.Execute(args.SelectedItem);
            }
        }

        private async void RefreshOnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            await ViewModel.LoadAsync();
        }

        protected override async void OnAppearing()
        {
            //if (!Util.CheckConnectivity())
            //{
            //    await Navigation.PushAsync(new OfflinePage());
            //}
            base.OnAppearing();
        }
    }
}
