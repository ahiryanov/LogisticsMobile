using LogisticsMobile.ViewModels;
using Plugin.Iconize;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LogisticsMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Iconize
                          .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                          .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                          .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                          .With(new Plugin.Iconize.Fonts.MaterialModule());


            MainPage = new LoginPage();
            MessagingCenter.Subscribe<LoginPageViewModel>(this, "AuthentificationPassed", (sender) => {
                    MainPage = new NavigationPage(new MainPage());
            });
            MessagingCenter.Subscribe<MainPageMaster>(this, "Logout", (sender) => {
                MainPage = new LoginPage();
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
