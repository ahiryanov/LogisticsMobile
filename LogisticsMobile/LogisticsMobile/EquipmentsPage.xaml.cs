using LogisticsMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogisticsMobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EquipmentsPage : ContentPage
	{
		public EquipmentsPage (Model model)
		{
			InitializeComponent ();
            EquipmentsPageViewModel epvm = new EquipmentsPageViewModel(model) { Navigation = this.Navigation };
            BindingContext = epvm;
		}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}