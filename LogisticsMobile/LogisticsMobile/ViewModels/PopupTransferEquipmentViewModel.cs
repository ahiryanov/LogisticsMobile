using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class PopupTransferEquipmentViewModel : INotifyPropertyChanged
    {
        private List<string> _allPositions;
        public PopupTransferEquipmentViewModel(List<string> allPositions)
        {
            ConfirmTransferPositionCommand = new Command(ConfirmTransferPosition);
            _allPositions = allPositions;
        }

        private void ConfirmTransferPosition()
        {
            ConfirmedPosition = Position;
            PopupNavigation.Instance.PopAsync();
        }

        public string ConfirmedPosition { get; set; }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (!string.IsNullOrEmpty(_position))
                {
                    Positions = new ObservableCollection<string>(_allPositions.Where(p => p.ToLower().Contains(_position.ToLower())));
                    IsVisibleListView = true;
                }
                else
                {
                    IsVisibleListView = false;
                }
            }
        }

        private string _selectedPosition;
        public string SelectedPosition
        {
            get { return _selectedPosition; }
            set
            {
                _selectedPosition = value;
                if (_selectedPosition != null)
                {
                    Position = _selectedPosition;
                    IsVisibleListView = false;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ConfirmTransferPositionCommand { get; private set; }
        public bool IsVisibleListView { get; set; } = false;
        public ObservableCollection<string> Positions { get; set; }
    }
}
