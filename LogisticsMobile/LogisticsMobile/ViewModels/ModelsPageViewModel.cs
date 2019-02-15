using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    public class ModelsPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public ICommand RefreshCommand { get; set; }

        private ServerController _ctrl = new ServerController();
        private ModelCount _selectedModel;
        private List<ModelCount> _allModels;
        private bool _isByPosition = false;
        private string _position;

        public ModelsPageViewModel(string category, string type)
        {
            RefreshCommand = new Command(() => LoadModelsAsync(category, type));
            LoadModelsAsync(category, type);
        }

        public ModelsPageViewModel(string position)
        {
            RefreshCommand = new Command(() => LoadModelsByPositionAsync(position));
            LoadModelsByPositionAsync(position);
            _position = position;
            _isByPosition = true;
        }

        private async Task LoadModelsByPositionAsync(string position)
        {
            IsBusy = true;
            await Task.Run(async () => _allModels = await _ctrl.GetModelsByPosition(position));
            Models = _allModels;
            IsBusy = false;
        }

        private async Task LoadModelsAsync(string category, string type)
        {
            IsBusy = true;
            await Task.Run(async () => _allModels = await _ctrl.GetModels(category, type));
            Models = _allModels;
            IsBusy = false;
        }

        public bool IsBusy { get; set; }

        public List<ModelCount> Models { get; set; }

        public ModelCount SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                if (_selectedModel != value)
                {
                    var tempModel = value;
                    _selectedModel = null;
                    OnPropertyChanged(nameof(SelectedModel));
                    if (tempModel != null)
                    {
                        if (!_isByPosition)
                            Navigation.PushAsync(new EquipmentsPage(tempModel));
                        else
                            Navigation.PushAsync(new EquipmentsPage(tempModel, _position));
                    }
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
                    Models = _allModels.Where(
                        r => r.Model.VendorName.ToLower().Contains(_searchingText.ToLower()) ||
                        r.Model.ModelName.ToLower().Contains(_searchingText.ToLower()) ||
                        r.Model.EquipmentType.ToLower().Contains(_searchingText.ToLower())).ToList();
                else
                    Models = _allModels;

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
