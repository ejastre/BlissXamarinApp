using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace BlissXamarinApp.Controls
{
    public class InfiniteListView : ListView
    {
        public static readonly BindableProperty LoadCommandProperty = BindableProperty.Create<InfiniteListView, ICommand>(bp => bp.LoadCommand, default(ICommand));

        public ICommand LoadCommand
        {
            get => (ICommand)this.GetValue(LoadCommandProperty);
            set => this.SetValue(LoadCommandProperty, value);
        }

        public InfiniteListView()
        {
            this.ItemAppearing += (object sender, ItemVisibilityEventArgs e) =>
            {
                var items = this.ItemsSource as IList;

                if (items != null && e.Item == items[items.Count - 1])
                    if (this.LoadCommand != null && this.LoadCommand.CanExecute(null))
                        this.LoadCommand.Execute(null);
            };
        }
    }
}
