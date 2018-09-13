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
	public partial class EquipmentInfoPage : ContentPage
	{
		public EquipmentInfoPage (Equipment equipment, bool isNewEquipment)
		{
			InitializeComponent ();

            EquipmentInfoPageViewModel viewmodel = new EquipmentInfoPageViewModel(equipment, isNewEquipment);

            if (isNewEquipment)
            {
                var saveNewToolbarItem = new ToolbarItem() { Text = "Save" };
                saveNewToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SaveNewEquipmentCommand"));
                ToolbarItems.Add(saveNewToolbarItem);

                viewmodel.IsEditing = true;
            }
            else
            {
                var saveExistToolbarItem = new ToolbarItem() { Text = "Save" };
                saveExistToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SaveExistEquipmentCommand"));
                var EditToolbarItem = new ToolbarItem() { Text = "Edit" };
                EditToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("EditClickCommand"));

                ToolbarItems.Add(saveExistToolbarItem);
                ToolbarItems.Add(EditToolbarItem);
            }

            
            BindingContext = viewmodel;
        }
	}
}