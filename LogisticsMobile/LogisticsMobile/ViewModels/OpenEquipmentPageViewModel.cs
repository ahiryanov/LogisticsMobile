using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LogisticsMobile.ViewModels
{
    class OpenEquipmentPageViewModel : INotifyPropertyChanged
    {
        private Equipment _equipment;
        public OpenEquipmentPageViewModel(Equipment equipment)
        {
            _equipment = equipment;
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



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
