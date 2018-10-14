using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class EquipmentHistoryPageViewModel : INotifyPropertyChanged
    {
        private List<Manager> _users;
        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private Equipment _equipment;
        private ObservableCollection<TransferEquipment> _histories;

        public EquipmentHistoryPageViewModel(Equipment equipment)
        {
            _equipment = equipment;
            LoadUsers();
            LoadHistory();
        }

        private async void LoadUsers()
        {
            _users = await _ctrl.GetAllUsers();
        }

        private async void LoadHistory()
        {
            IsBusy = true;
            Histories = new ObservableCollection<TransferEquipment>(await _ctrl.GetHistory(_equipment));
            IsBusy = false;
        }

        private TransferEquipment _selectedHistory;
        public TransferEquipment SelectedHistory
        {
            get { return _selectedHistory; }
            set
            {
                if (value != null)
                {
                    _selectedHistory = value;
                    var user = _users.Find(r => r.idManager == SelectedHistory.idManager);
                    string message = string.Format("Откуда: {0} \nКуда: {1} \nКто: {2} {3} \nКогда: {4:dd.MM.yyyy HH:mm}", SelectedHistory.TransferFrom, SelectedHistory.TransferTo, user.family, user.name, SelectedHistory.TransferDateTime);
                    Application.Current.MainPage.DisplayAlert("Перемещение", message, "OK");
                }
            }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public bool IsLoaded
        {
            get { return !_isBusy; }
        }

        public ObservableCollection<TransferEquipment> Histories
        {
            get { return _histories; }
            set
            {
                _histories = value;
                OnPropertyChanged(nameof(Histories));
            }
        }
       

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
