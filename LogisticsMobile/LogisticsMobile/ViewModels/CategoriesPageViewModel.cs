using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class CategoryesPageViewModel : INotifyPropertyChanged
    {
        public enum States
        {
            Categories,
            SearchedModels
        }
        public INavigation Navigation { get; set; }

        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private List<string> _categories;
        private ObservableCollection<ModelCount> _allModel;
        private string _selectedCategory;

        public CategoryesPageViewModel()
        {
            LoadCategories();
            LoadModels();
        }

        private async void LoadModels()
        {
            _allModel = new ObservableCollection<ModelCount>(await _ctrl.GetAllModels());
        }

        private async void LoadCategories()
        {
            IsBusy = true;
            Categories = await _ctrl.GetCategories();
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
        
        private string _searchingText;
        public string SearchingText
        {
            get { return _searchingText; }
            set
            {
                if (_searchingText != value)
                {
                    _searchingText = value;
                    OnPropertyChanged(nameof(SearchingText));
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        State = States.SearchedModels;
                        SearchModels(_searchingText);
                    }
                    else
                    {
                        State = States.Categories;
                    }
                }
                
            }
        }

        private void SearchModels(string searchingText)
        {
            SearchedModels = new ObservableCollection<ModelCount>(
                _allModel.Where(
                    r => r.Model.VendorName.ToLower().Contains(searchingText.ToLower()) ||
                    r.Model.ModelName.ToLower().Contains(searchingText.ToLower()) ||
                    r.Model.EquipmentType.ToLower().Contains(searchingText.ToLower()
                    )));
        }

        public ObservableCollection<ModelCount> SearchedModels { get; set; }

        private ModelCount _selectedModel;
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
                        Navigation.PushAsync(new EquipmentsPage(tempModel));
                }
            }
        }

        public List<string> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    var tempCategory = value;
                    _selectedCategory = null;
                    OnPropertyChanged(nameof(SelectedCategory));
                    if (tempCategory != null)
                        Navigation.PushAsync(new TypesPage(tempCategory));
                }
            }
        }

        public States State { get; set; } = States.Categories;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
