﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Rg.Plugins.Popup.Services;
using System.Threading;
using Plugin.Settings;

namespace LogisticsMobile.ViewModels
{
    public class MultiScannerPageViewModel : INotifyPropertyChanged
    {
        private ServerController _ctrl = new ServerController();
        private bool _popupOpen = false;
        public INavigation Navigation { get; set; }
        public ICommand DeleteEquipmentCommand { protected set; get; }
        public ICommand TransferEquipmentsCommand { get; private set; }
        public ICommand QRScanResultCommand { private set; get; }

        public MultiScannerPageViewModel()
        {
            DeleteEquipmentCommand = new Command(DeleteEquipment);
            TransferEquipmentsCommand = new Command(TransferEquipments);
            QRScanResultCommand = new Command(Scanning);
        }

        private async void TransferEquipments(object obj)
        {
            if (!_popupOpen)
            {
                _popupOpen = true;
                var popupPage = new PopupTransferEquipment(await _ctrl.GetPositions());
                popupPage.Disappearing += PopupPage_DisappearingAsync;
                await PopupNavigation.Instance.PushAsync(popupPage);
            }
        }

        private async void PopupPage_DisappearingAsync(object sender, EventArgs e)
        {
            _popupOpen = false;
            SelectedPosition = ((sender as PopupTransferEquipment).BindingContext as PopupTransferEquipmentViewModel).ConfirmedPosition;
            if(!string.IsNullOrEmpty(SelectedPosition) && ScannedEquipments.Count > 0)
            {
                int.TryParse(CrossSettings.Current.GetValueOrDefault("UserID", null), out _userID);
                if (await _ctrl.TransferEquipments(ScannedEquipments.ToList(), _userID, SelectedPosition) != null)
                {
                    DependencyService.Get<IMessage>().LongAlert("Оборудование перемещено");
                    foreach (var eq in ScannedEquipments)
                        eq.PositionState = SelectedPosition;
                    ScannedEquipments = new ObservableCollection<Equipment>();
                    SelectedPosition = null;
                }
                else
                    DependencyService.Get<IMessage>().ShortAlert("Ошибка перемещения");
            }
        }

        private async void Scanning()
        {
            IsAnalyzing = false;
            if (ScannedEquipments?.Where(e => e.ISNumber == Result.Text).Count() == 0)
            {
                var addedEquipment = new List<Equipment>();

                IsBusy = true;
                await Task.Run(async () =>
                  {
                      addedEquipment = await _ctrl.GetEquipment(Result?.Text);
                      foreach (Equipment eq in addedEquipment)
                      {
                          eq.Model = await _ctrl.GetModel(eq.IDModel);
                      }
                  });
                
                IsBusy = false;

                switch (addedEquipment?.Count)
                {
                    case 1:
                        Vibration.Vibrate(TimeSpan.FromMilliseconds(50));
                        ScannedEquipments.Add(addedEquipment.FirstOrDefault());
                        IsAnalyzing = true;
                        break;
                    case 0:
                        DependencyService.Get<IMessage>().ShortAlert("Не найдено!");
                        Thread.Sleep(100);
                        IsAnalyzing = true;
                        break;
                    default:
                        DependencyService.Get<IMessage>().LongAlert("Несколько едениц оборудования с этим ИСН");
                        Thread.Sleep(100);
                        IsAnalyzing = true;
                        break;
                }
            }
            else
            {
                Thread.Sleep(100);
                IsAnalyzing = true;
            }
        }

        private void DeleteEquipment(object obj)
        {
            if (obj != null)
            {
                ScannedEquipments.Remove(obj as Equipment);
                DependencyService.Get<IMessage>().LongAlert("Удалено!");
            }
        }

        private Equipment _selectedEquipment;
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                if (_selectedEquipment != null)
                {
                    IsScanning = false;
                    IsAnalyzing = false;
                    var openEquipment = new OpenEquipmentPage(_selectedEquipment);
                    openEquipment.Disappearing += OpenEquipment_Disappearing;
                    Navigation.PushAsync(openEquipment);
                }
            }
        }

        private void OpenEquipment_Disappearing(object sender, EventArgs e)
        {
            IsScanning = true;
            IsAnalyzing = true;
        }

        public string SelectedPosition { get; set; }
        public ObservableCollection<Equipment> ScannedEquipments { get; set; } = new ObservableCollection<Equipment>();
        public bool IsBusy { get; set; }
        public bool IsTorchOn { get; set; }
        public bool IsAnalyzing { get; set; } = true;
        public bool IsScanning { get; set; }
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _userID;
    }
}