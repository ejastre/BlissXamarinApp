using BlissXamarinApp.ViewModels;
using Xamarin.Forms;

namespace BlissXamarinApp.Views
{
    public partial class QuestionPage
    {
        private QuestionViewModel ViewModel => BindingContext as QuestionViewModel;

        public QuestionPage()
        {
            InitializeComponent();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args == null) return; // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row

            if (args.SelectedItem != null)
            {
                ViewModel.AnswerCommand.Execute(args.SelectedItem);
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width < 100) return;

            //Calculate screen size for answers
        }
    }
}
