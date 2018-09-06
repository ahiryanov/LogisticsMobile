using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class ModelsPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private List<Model> _models;
        private string _selectedModel;
        private string _category;
        private string _type;

        public ModelsPageViewModel(string category, string type)
        {
            _category = category;
            _type = type;
            LoadModels();
           // Categories = await ctrl.GetCategories();
        }

        private async void LoadModels()
        {
            IsBusy = true;
            Models = await _ctrl.GetModels(_category,_type);
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

        public List<Model> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                OnPropertyChanged(nameof(Models));
            }
        }

        public string SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                if (_selectedModel != value)
                {
                    var tempModel = value;
                    _selectedModel = null;
                    OnPropertyChanged(nameof(SelectedModel));
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
