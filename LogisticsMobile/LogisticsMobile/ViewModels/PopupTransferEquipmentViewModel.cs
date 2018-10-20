using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LogisticsMobile.ViewModels
{
    class PopupTransferEquipmentViewModel : INotifyPropertyChanged
    {
        public bool IsVisibleListView { get; set; } = false;
        public ObservableCollection<string> Positions { get; set; }

        private List<string> _allPositions;
        public PopupTransferEquipmentViewModel(List<string> allPositions)
        {
            _allPositions = allPositions;
        }

        private string _position;

        public event PropertyChangedEventHandler PropertyChanged;

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
                if(_selectedPosition!=null)
                    Position = _selectedPosition;
            }
        }
    }
}
