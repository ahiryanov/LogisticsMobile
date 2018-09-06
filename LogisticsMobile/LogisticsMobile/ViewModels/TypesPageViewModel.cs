﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class TypesPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;

        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private List<string> _types;
        private string _selectedType;
        private string _category;

        public TypesPageViewModel(string category)
        {
            _category = category;
            LoadTypes();
           // Categories = await ctrl.GetCategories();
        }

        private async void LoadTypes()
        {
            IsBusy = true;
            Types = await _ctrl.GetTypes(_category);
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

        public List<string> Types
        {
            get { return _types; }
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
            }
        }

        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (_selectedType != value)
                {
                    var tempType = value;
                    _selectedType = null;
                    OnPropertyChanged(nameof(SelectedType));
                    if (tempType != null)
                        Navigation.PushAsync(new TypesPage(tempType));
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