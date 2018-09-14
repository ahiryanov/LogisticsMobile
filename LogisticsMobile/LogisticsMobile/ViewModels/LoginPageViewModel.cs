using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public ICommand LoginButtonCommand { get; protected set; }
        private ServerController _ctrl = new ServerController();
        public LoginPageViewModel()
        {
            object user;
            if (App.Current.Properties.TryGetValue("User", out user))
            {
                MessagingCenter.Send(this, "AuthentificationPassed");
                // выполняем действия, если в словаре есть ключ "name"
            }
            LoginButtonCommand = new Command(LoginButton);
        }

        private async void LoginButton()
        {
            var authUser = new Manager();
            authUser.family = Family;
            authUser.name = Name;
            authUser.password = Password;
            var validUser = await _ctrl.AuthUser(authUser);
            if (validUser != null)
            {
                if(IsStayLogin)
                    App.Current.Properties["User"] = validUser;
                MessagingCenter.Send(this, "AuthentificationPassed");
            }
            else
                MessagingCenter.Send(this, "AuthentificationFailed");
        }

        public string Family { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsStayLogin { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}
