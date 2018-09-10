using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LogisticsMobile.ViewModels
{
    class EquipmentInfoPageViewModel : INotifyPropertyChanged
    {
        ServerController _ctrl = new ServerController();
        private Equipment _equipment;
        private List<string> _positions;
        private List<string> _healths;
        public EquipmentInfoPageViewModel(Equipment equipment)
        {
            _equipment = equipment;
            LoadPositions();
            LoadHealths();
        }

        private async void LoadHealths()
        {
           // Healths = new List<string>();
           // Healths = await _ctrl.GetHealths();
        }

        private async void LoadPositions()
        {
           // Positions = new List<string>();
           // Positions = await _ctrl.GetPositions();
        }

        public Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                _equipment = value;
                OnPropertyChanged(nameof(Equipment));
            }
        }

        public List<string> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                OnPropertyChanged(nameof(Positions));
            }
        }

        public List<string> Healths
        {
            get { return _healths; }
            set
            {
                _healths = value;
                OnPropertyChanged(nameof(Healths));
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
