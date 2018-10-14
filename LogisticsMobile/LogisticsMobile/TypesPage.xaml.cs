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
	public partial class TypesPage : ContentPage
	{
		public TypesPage (string category)
		{
			InitializeComponent ();
            TypesPageViewModel tpvm = new TypesPageViewModel(category) { Navigation = this.Navigation };
            BindingContext = tpvm;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}