using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogisticsMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PositionListsPage : ContentPage
	{
		public PositionListsPage()
		{
			InitializeComponent();
            BindingContext = new PositionListsPageViewModel() { Navigation = this.Navigation };
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}