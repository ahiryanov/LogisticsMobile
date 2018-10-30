using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile
{
    public class PositionListsPageViewModel : INotifyPropertyChanged
    {

        private ServerController _ctrl = new ServerController();
        private ObservableCollection<string> _allPositions;

        public INavigation Navigation { get; set; }
        public bool IsBusy { get; set; }
        public ObservableCollection<string> Positions { get; set; }
        public ICommand RefreshCommand { get; private set; }

        private string _selectedPosition;
        public string SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                _selectedPosition = value;
                if(_selectedPosition != null)
                {
                    Navigation.PushAsync(new ModelsPage(_selectedPosition));
                }
            }
        }

        private string _searchingText;
        public string SearchingText
        {
            get => _searchingText;
            set
            {
                _searchingText = value;
                if (!string.IsNullOrEmpty(_searchingText))
                {
                    Positions = new ObservableCollection<string>(_allPositions.Where(p => p.ToLower().Contains(_searchingText.ToLower())));
                }
                else
                    Positions = _allPositions;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public PositionListsPageViewModel()
        {
            LoadPositionsAsync();
            RefreshCommand = new Command(LoadPositionsAsync);
        }

        private async void LoadPositionsAsync()
        {
            IsBusy = true;
            await Task.Run(async () => _allPositions = new ObservableCollection<string>(await _ctrl.GetPositions()));
            Positions = _allPositions;
            IsBusy = false;
        }
    }
}
