using Plugin.Settings;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public ICommand LoginButtonCommand { get; protected set; }
        public ICommand ReturnLoginViewCommand { get; protected set; }
        private ServerController _ctrl = new ServerController();
        public LoginPageViewModel()
        {
            LoginButtonCommand = new Command(LoginButton);
            ReturnLoginViewCommand = new Command(() => State = "Login");
            if (CheckCredentials())
            {
                var authUser = new Manager();
                authUser.family = CrossSettings.Current.GetValueOrDefault("Family", null);
                authUser.name = CrossSettings.Current.GetValueOrDefault("Name", null);
                authUser.password = CrossSettings.Current.GetValueOrDefault("Password", null);
                CheckAuth(authUser);
            }
        }

        private async void LoginButton()
        {
            var authUser = new Manager();
            authUser.family = Family;
            authUser.name = Name;
            authUser.password = Password;

            State = "Loading";
            Thread.Sleep(3000);
            var checkResult = await CheckAuth(authUser);
            State = "Login";
            if (checkResult)
                MessagingCenter.Send(this, "AuthentificationPassed");
            else
                State = "AuthentificationFailed";
        }

        private async Task<bool> CheckAuth(Manager authUser)
        {
            var validUser = await _ctrl.AuthUser(authUser);
            if (validUser != null)
            {
                if (IsStayLogin)
                {
                    CrossSettings.Current.AddOrUpdateValue("Family", validUser.family);
                    CrossSettings.Current.AddOrUpdateValue("Name", validUser.name);
                    CrossSettings.Current.AddOrUpdateValue("Password", validUser.password);
                }
                return true;    
            }
            return false;
        }

        public string Family { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsStayLogin { get; set; }
        public string State { get; set; } = "Login";

        private bool CheckCredentials()
        {
            return CrossSettings.Current.Contains("User") && CrossSettings.Current.Contains("Family") && CrossSettings.Current.Contains("Password");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
