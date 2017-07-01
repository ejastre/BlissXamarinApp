using BlissXamarinApp.Interfaces;
using BlissXamarinApp.Models;
using BlissXamarinApp.Utils;
using Xamarin.Forms;

namespace BlissXamarinApp.ViewModels
{
    public class ShareViewModel : BaseViewModel
    {
        private readonly IBlissApiService _blissXamarinApiService;

        private readonly string _contentUrl;
        private string _destinationEmail;

        public string DestinationEmail
        {
            get => _destinationEmail;
            set
            {
                SetProperty(ref _destinationEmail, value);
                ShareCommand.ChangeCanExecute();
            }
        }

        public Command ShareCommand { get; }

        public ShareViewModel(IBlissApiService blissXamarinApiService, string contentUrl)
        {
            Title = "Share Page";

            _blissXamarinApiService = blissXamarinApiService;
            _contentUrl = contentUrl;

            ShareCommand = new Command(ExecuteShareCommand, CanExecuteShareCommand);
        }

        private bool CanExecuteShareCommand()
        {
            return !string.IsNullOrWhiteSpace(DestinationEmail) && Util.CheckEmail(DestinationEmail);
        }

        private async void ExecuteShareCommand()
        {
            var content = new ShareScreen
            {
                DestinationEmail = _destinationEmail,
                ContentUrl = $"blissrecruitment://questions{_contentUrl}"
            };
            var result = await _blissXamarinApiService.PostUrlAsync(content);

            if (result)
                await DisplayAlert("Shared successfully", "Your screen was shared!", "Ok");
            else
                await DisplayAlert("Error", "Sorry! Could not share, try again later!", "Ok");
        }
    }
}
