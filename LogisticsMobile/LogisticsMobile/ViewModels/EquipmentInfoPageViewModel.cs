using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace LogisticsMobile.ViewModels
{
    class EquipmentInfoPageViewModel : INotifyPropertyChanged
    {
        ServerController _ctrl = new ServerController();
        private Equipment _equipment;
        private ObservableCollection<string> _positions;
        private ObservableCollection<string> _healths;
        private ObservableCollection<string> _assignedPositions;
        private string _selectedPosition;
        private string _selectedHealth;
        private string _selectedAssignedPosition;

        public EquipmentInfoPageViewModel(Equipment equipment)
        {
            _equipment = equipment;
            LoadPositions();
            LoadHealths();
            LoadAssignedPositions();
        }

        private async void LoadPositions()
        {
            Positions = new ObservableCollection<string>(await _ctrl.GetPositions());
            SelectedPosition = _equipment.PositionState;
        }

        private async void LoadHealths()
        {
            Healths = new ObservableCollection<string>(await _ctrl.GetHealths());
            SelectedHealth = _equipment.HealthState;
        }
        
        private void LoadAssignedPositions()
        {
            AssignedPositions = new ObservableCollection<string>(GlobalVariables.AssignedPositions);
            SelectedAssignedPosition = _equipment.AssignedPosition;
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
            get { return _selectedPosition; }
            set
            {
                if (_selectedPosition != value)
                {
                    _selectedPosition = value;
                    OnPropertyChanged(nameof(SelectedPosition));
                }
            }
        }

        public string SelectedHealth
        {
            get { return _selectedHealth; }
            set
            {
                if (_selectedHealth != value)
                {
                    _selectedHealth = value;
                    OnPropertyChanged(nameof(SelectedHealth));
                }
            }
        }

        public string SelectedAssignedPosition
        {
            get { return _selectedAssignedPosition; }
            set
            {
                if (_selectedAssignedPosition != value)
                {
                    _selectedAssignedPosition = value;
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
