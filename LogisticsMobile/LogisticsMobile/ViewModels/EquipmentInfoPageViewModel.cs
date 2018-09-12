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
        ServerController _ctrl = new ServerController();

        public ICommand EditClickCommand { get; protected set; }
        public ICommand SaveClickCommand { get; protected set; }

        private bool _isEditing = false;
        private bool _isNewEquipment;
        private Equipment _equipment;
        private ObservableCollection<string> _positions;
        private ObservableCollection<string> _healths;
        private ObservableCollection<string> _assignedPositions;
        private string _selectedPosition;
        private string _selectedHealth;
        private string _selectedAssignedPosition;

        public EquipmentInfoPageViewModel(Equipment equipment,bool isNewEquipment)
        {
            EditClickCommand = new Command(EditClick);
            SaveClickCommand = new Command(SaveClick);
            _isNewEquipment = isNewEquipment;
            _equipment = equipment;
            LoadPositions();
            LoadHealths();
            LoadAssignedPositions();
            if (isNewEquipment)
            {
                IsEditing = true;
            }
        }

        public bool IsNewEquipment
        {
            get { return _isNewEquipment; }
            set
            {
                if (_isNewEquipment != value)
                {
                    _isNewEquipment = value;
                    OnPropertyChanged(nameof(IsNewEquipment));
                }
            }
        }

        private async void SaveClick(object obj)
        {
            
                Equipment returnedObj = await _ctrl.UpdateEquipment(_equipment);
            
            if (returnedObj != null)
            {
                MessageText = "Сохранено!";
                MessageColor = Color.Green;
                IsShowMessage = true;
                await Task.Delay(3000);
                IsShowMessage = false;
            }
            else
            {
                MessageText = "Ошибка!";
                MessageColor = Color.Red;
                IsShowMessage = true;
                await Task.Delay(3000);
                IsShowMessage = false;
            }
        }

        private Color _messageColor;
        public Color MessageColor
        {
            get { return _messageColor; }
            set
            {
                if(_messageColor!=value)
                {
                    _messageColor = value;
                    OnPropertyChanged(nameof(MessageColor));
                }
            }
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                if (_messageText != value)
                {
                    _messageText = value;
                    OnPropertyChanged(nameof(MessageText));
                }
            }
        }

        private bool _isShowMessage;
        public bool IsShowMessage
        {
            get { return _isShowMessage; }
            set
            {
                if (_isShowMessage != value)
                {
                    _isShowMessage = value;
                    OnPropertyChanged(nameof(IsShowMessage));
                }
            }
        }

        private void EditClick()
        {
            IsEditing = !IsEditing;
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
