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
	public partial class ModelsPage : ContentPage
	{
		public ModelsPage (string category, string type)
		{
			InitializeComponent ();
            ModelsPageViewModel mpvm = new ModelsPageViewModel(category, type) { Navigation = this.Navigation };
            BindingContext = mpvm;
		}
	}
}