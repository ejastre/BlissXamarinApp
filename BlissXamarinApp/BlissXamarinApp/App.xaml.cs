using Xamarin.Forms;

namespace BlissXamarinApp
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage())
            {
                BarBackgroundColor = (Color)Current.Resources["BarBackgroundColor"],
                BarTextColor = (Color)Current.Resources["BarTextColor"]
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
