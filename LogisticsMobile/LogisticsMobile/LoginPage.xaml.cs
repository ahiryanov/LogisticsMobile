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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            BindingContext = new LoginPageViewModel();
         //   MessagingCenter.Subscribe<LoginPageViewModel>(this,"AuthentificationFailed", (sender) => {
          //          DisplayAlert("Ошибка", "Не пройдёшь", "ОК");
           // });
        }
	}
}