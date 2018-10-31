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
			InitializeComponent();
            BindingContext = new ModelsPageViewModel(category, type) { Navigation = this.Navigation };
        }

        public ModelsPage(string position)
        {
            InitializeComponent();
            Title = position;
            BindingContext = new ModelsPageViewModel(position) { Navigation = this.Navigation };
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
        }
    }
}