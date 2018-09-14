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
        bool exist = false;
        public EquipmentInfoPage (Equipment equipment, bool isNewEquipment)
		{
			InitializeComponent ();

            EquipmentInfoPageViewModel viewmodel = new EquipmentInfoPageViewModel(equipment, isNewEquipment) { Navigation = this.Navigation };

            if (isNewEquipment)
            {
                var saveNewToolbarItem = new ToolbarItem() { Text = "Save" };
                saveNewToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SaveNewEquipmentCommand"));
                ToolbarItems.Add(saveNewToolbarItem);

                viewmodel.IsEditing = true;
                PositionPicker.InputTransparent = false;
                AssignedPositionPicker.InputTransparent = false;
            }
            else
            {
                var EditToolbarItem = new ToolbarItem() { Text = "Edit" };
                EditToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("EditClickCommand"));
                EditToolbarItem.Clicked += EditItem_Clicked;
                ToolbarItems.Add(EditToolbarItem);
            }
            BindingContext = viewmodel;
        }

        private void EditItem_Clicked(object sender, EventArgs e)
        {
            if (!exist)
            {
                var saveExistToolbarItem = new ToolbarItem() { Text = "Save" };
                saveExistToolbarItem.SetBinding(ToolbarItem.CommandProperty, new Binding("SaveExistEquipmentCommand"));
                ToolbarItems.Insert(0,saveExistToolbarItem);
                exist = true;
            }
        }
    }
}