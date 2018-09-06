using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class EquipmentsPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private List<Equipment> _equipments;
        private Equipment _selectedEquipment;
        private Model _model;

        public EquipmentsPageViewModel(Model model)
        {
            _model = model;
            LoadEquipments();
           // Categories = await ctrl.GetCategories();
        }

        private async void LoadEquipments()
        {
            IsBusy = true;
            Equipments = await _ctrl.GetEquipments(_model);
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        public bool IsLoaded
        {
            get { return !_isBusy; }
        }

        public List<Equipment> Equipments
        {
            get { return _equipments; }
            set
            {
                _equipments = value;
                OnPropertyChanged(nameof(Equipments));
            }
        }

        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                if (_selectedEquipment != value)
                {
                    var tempModel = value;
                    _selectedEquipment = null;
                    OnPropertyChanged(nameof(SelectedEquipment));
                    //if (tempModel != null) 
                       // Navigation.PushAsync(new EquipmentsPage(tempModel));
                }
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
