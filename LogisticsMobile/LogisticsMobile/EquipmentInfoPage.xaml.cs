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
		public EquipmentInfoPage (Equipment equipment)
		{
			InitializeComponent ();
            EquipmentInfoPageViewModel viewmodel = new EquipmentInfoPageViewModel(equipment);
            BindingContext = viewmodel;
		}
	}
}