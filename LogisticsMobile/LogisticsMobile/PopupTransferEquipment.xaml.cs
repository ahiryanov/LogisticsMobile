using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;
using LogisticsMobile.ViewModels;
using Xamarin.Forms;

namespace LogisticsMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupTransferEquipment : PopupPage
    {
		public PopupTransferEquipment (List<string> allPositions)
		{
			InitializeComponent ();
            BindingContext = new PopupTransferEquipmentViewModel(allPositions);
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}