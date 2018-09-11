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
        public ICommand RefreshPullCommand { get; }

        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private List<Equipment> _equipments;
        private Equipment _selectedEquipment;
        private Model _model;

        public EquipmentsPageViewModel(Model model)
        {
            _model = model;
            RefreshPullCommand = new Command(LoadEquipments);
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
            }
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
                    var tempEquipment = value;
                    _selectedEquipment = null;
                    OnPropertyChanged(nameof(SelectedEquipment));
                    if (tempEquipment != null) 
                        Navigation.PushAsync(new OpenEquipmentPage(tempEquipment));
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
