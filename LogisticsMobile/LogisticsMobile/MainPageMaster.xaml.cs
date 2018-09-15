using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Settings;

namespace LogisticsMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public ListView ListView;

        public MainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MainPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageMenuItem> MenuItems { get; set; }

            public MainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MainPageMenuItem>(new[]
                {
                    new MainPageMenuItem { Id = 0, Title = "Каталог", TargetType = typeof(CategoriesPage) },
                    new MainPageMenuItem { Id = 1, Title = "Настройки", TargetType = typeof(SettingsPage) },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            CrossSettings.Current.Remove("Family");
            CrossSettings.Current.Remove("Name");
            CrossSettings.Current.Remove("Password");
            MessagingCenter.Send(this, "Logout");
        }

    }

    public class MainPageMenuItem
    {
        //   public MainPageMenuItem()
        //    {
        //      TargetType = typeof(MainPageDetail);
        //   }
        public int Id { get; set; }
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}