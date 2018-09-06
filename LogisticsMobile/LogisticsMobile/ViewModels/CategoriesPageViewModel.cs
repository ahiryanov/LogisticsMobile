using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class CategoryesPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        ServerController ctrl = new ServerController();
        private bool _isBusy;
        private List<string> _categories;
        private string _selectedCategory;

        public CategoryesPageViewModel()
        {
            LoadCategories();
        }

        private async void LoadCategories()
        {
            IsBusy = true;
            Categories = await ctrl.GetCategories();
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
                //OnPropertyChanged(nameof(IsLoaded));
            }
        }

     /*   public bool IsLoaded
        {
            get { return !isBusy; }
        }*/

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
