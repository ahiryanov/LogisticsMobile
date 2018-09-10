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
        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private Equipment _equipment;
        private ObservableCollection<TransferEquipment> _histories;

        public EquipmentHistoryPageViewModel(Equipment equipment)
        {
            _equipment = equipment;
            LoadHistory();
        }

        private async void LoadHistory()
        {
            IsBusy = true;
            Histories = new ObservableCollection<TransferEquipment>(await _ctrl.GetHistory(_equipment));
            IsBusy = false;
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
