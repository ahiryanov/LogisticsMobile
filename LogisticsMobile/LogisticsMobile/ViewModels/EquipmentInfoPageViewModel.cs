using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace LogisticsMobile.ViewModels
{
    class EquipmentInfoPageViewModel : INotifyPropertyChanged
    {
        public event EventHandler SaveNewSuccess;
        public event EventHandler SaveExistSuccess;
        public event EventHandler SaveError;
        private void OnSaveNewSucces() => SaveNewSuccess?.Invoke(this, EventArgs.Empty);
        private void OnSaveExistSucces() => SaveExistSuccess?.Invoke(this, EventArgs.Empty);
        private void OnSaveError() => SaveError?.Invoke(this, EventArgs.Empty);

        public INavigation Navigation { get; set; }
        ServerController _ctrl = new ServerController();

        public ICommand EditClickCommand { get; protected set; }
        public ICommand SaveNewEquipmentCommand { get; protected set; }
        public ICommand SaveExistEquipmentCommand { get; protected set; }

        

        private bool _isEditing = false;
        //private bool _isNewEquipment;
        private Equipment _equipment;
        private ObservableCollection<string> _positions;
        private ObservableCollection<string> _healths;
        private ObservableCollection<string> _assignedPositions;

        public EquipmentInfoPageViewModel(Equipment equipment,bool isNewEquipment)
        {
            _equipment = equipment;
            EditClickCommand = new Command(EditClick);
            SaveNewEquipmentCommand = new Command(SaveNew);
            SaveExistEquipmentCommand = new Command(SaveExist);
            //_isNewEquipment = isNewEquipment;
            LoadPositions();
            LoadHealths();
            LoadAssignedPositions();
        }

        private async void SaveExist(object obj)
        {
            Equipment returnedObj = await _ctrl.UpdateEquipment(_equipment);
            if (returnedObj.IDEquipment == _equipment.IDEquipment)
            {
                MessagingCenter.Send(this, "EquipmentsInfoPage", "SaveExist");
                await Navigation.PopAsync();
            }
            else
                MessagingCenter.Send(this, "EquipmentsInfoPage", "SaveError");
        }

        

        private async void SaveNew(object obj)
        {
            Equipment returnedObj = await _ctrl.AddEquipment(_equipment);
            if (returnedObj != null)
            {
                _equipment = returnedObj;
                OnSaveNewSucces();
                await Navigation.PopAsync();
            }
            else
                OnSaveError();
        }

        private void EditClick()
        {
            IsEditing = !IsEditing;
        }

        private async void LoadPositions()
        {
            Positions = new ObservableCollection<string>(await _ctrl.GetPositions());
            OnPropertyChanged(nameof(SelectedPosition));;
        }

        private async void LoadHealths()
        {
            Healths = new ObservableCollection<string>(await _ctrl.GetHealths());
            OnPropertyChanged(nameof(SelectedHealth));
        }

        private void LoadAssignedPositions()
        {
            AssignedPositions = new ObservableCollection<string>(GlobalVariables.AssignedPositions);
        }

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                if (_isEditing != value)
                {
                    _isEditing = value;
                    OnPropertyChanged(nameof(IsEditing));
                    OnPropertyChanged(nameof(IsNotEditing));
                }
            }
        }

        public bool IsNotEditing
        {
            get { return !_isEditing; }
        }

        public Equipment Equipment
        {
            get { return _equipment; }
            set
            {
                if (_equipment != value)
                {
                    _equipment = value;
                    OnPropertyChanged(nameof(Equipment));
                }
            }
        }

        public ObservableCollection<string> Positions
        {
            get { return _positions; }
            set
            {
                if (_positions != value)
                {
                    _positions = value;
                    OnPropertyChanged(nameof(Positions));
                }
            }
        }

        public ObservableCollection<string> Healths
        {
            get { return _healths; }
            set
            {
                _healths = value;
                OnPropertyChanged(nameof(Healths));
            }
        }

        public ObservableCollection<string> AssignedPositions
        {
            get { return _assignedPositions; }
            set
            {
                if (_assignedPositions != value)
                {
                    _assignedPositions = value;
                    OnPropertyChanged(nameof(AssignedPositions));
                }

            }
        }

        public string SelectedPosition
        {
            get { return _equipment.PositionState; }
            set
            {
                if (_equipment.PositionState != value && value != null)
                {
                    _equipment.PositionState = value;
                    OnPropertyChanged(nameof(SelectedPosition));
                }
            }
        }

        public string SelectedHealth
        {
            get { return _equipment.HealthState; }
            set
            {
                if (_equipment.HealthState != value && value != null)
                {
                    _equipment.HealthState = value;
                    OnPropertyChanged(nameof(SelectedHealth));
                }
            }
        }

        public string SelectedAssignedPosition
        {
            get { return _equipment.AssignedPosition; }
            set
            {
                if (_equipment.AssignedPosition != value && value != null)
                {
                    _equipment.AssignedPosition = value;
                    OnPropertyChanged(nameof(SelectedAssignedPosition));
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
