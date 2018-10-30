using LogisticsMobile.ViewModels;
using Plugin.Iconize.Fonts;
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
		public EquipmentsPage (ModelCount modelCount)
		{
			InitializeComponent ();

            var addToolbarItem = new ToolbarItem() { Text = "Add" };
            addToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("AddEquipmentCommand"));
            ToolbarItems.Add(addToolbarItem);

            EquipmentsPageViewModel epvm = new EquipmentsPageViewModel(modelCount.Model) { Navigation = this.Navigation };
            BindingContext = epvm;
            Title = string.Format("{0} {1} ({2} шт)", modelCount.Model.VendorName, modelCount.Model.ModelName, modelCount.Count);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}