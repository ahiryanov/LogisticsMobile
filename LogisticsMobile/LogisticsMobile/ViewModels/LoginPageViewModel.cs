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
        private enum States {
            Login,
            AuthentificationFailed,
            Loading }
        public ICommand LoginButtonCommand { get; protected set; }
        public ICommand ReturnLoginViewCommand { get; protected set; }
        private ServerController _ctrl = new ServerController();
        public LoginPageViewModel()
        {
            LoginButtonCommand = new Command(LoginAction);
            ReturnLoginViewCommand = new Command(() => State = States.Login.ToString());
            if (CheckCredentials())
            {
                var authUser = new Manager();
                Family = CrossSettings.Current.GetValueOrDefault("Family", null);
                Name = CrossSettings.Current.GetValueOrDefault("Name", null);
                Password = CrossSettings.Current.GetValueOrDefault("Password", null);
                LoginAction();
            }
        }

        private async void LoginAction()
        {
            var authUser = new Manager();
            authUser.family = Family;
            authUser.name = Name;
            authUser.password = Password;
            var validUser = await CheckAuth(authUser);
            if (validUser != null)
            {
                SaveValidUser(validUser);
                MessagingCenter.Send(this, "AuthentificationPassed");
            }
            else
                State = States.AuthentificationFailed.ToString();
        }

        private void SaveValidUser(Manager validUser)
        {
            CrossSettings.Current.AddOrUpdateValue("Family", validUser.family);
            CrossSettings.Current.AddOrUpdateValue("Name", validUser.name);
            CrossSettings.Current.AddOrUpdateValue("Password", validUser.password);
            CrossSettings.Current.AddOrUpdateValue("IsStayLogin", IsStayLogin);
        }

        private async Task<Manager> CheckAuth(Manager authUser)
        {
            State = States.Loading.ToString();
            var validUser = await _ctrl.AuthUser(authUser);
            //State = States.Login.ToString();
            if (validUser != null)
                return validUser;    
            return null;
        }

        public string Family { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsStayLogin { get; set; }
        public string State { get; set; } = States.Login.ToString();

        private bool CheckCredentials()
        {
            return 
                CrossSettings.Current.Contains("Family") && 
                CrossSettings.Current.Contains("Name") && 
                CrossSettings.Current.Contains("Password") &&
                CrossSettings.Current.GetValueOrDefault("IsStayLogin",false);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
