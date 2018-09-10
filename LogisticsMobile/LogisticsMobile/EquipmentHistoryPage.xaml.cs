using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogisticsMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EquipmentHistoryPage : ContentPage
	{
        private Equipment _equipment;
		public EquipmentHistoryPage (Equipment equipment)
		{
			InitializeComponent ();
            _equipment = equipment;
		}

        protected override void OnAppearing()
        {
            EquipmentHistoryPageViewModel viewmodel = new EquipmentHistoryPageViewModel(_equipment);
            BindingContext = viewmodel;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}