using System;
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
            var popupPage = new PopupTransferEquipment(await _ctrl.GetPositions());
            popupPage.Disappearing += PopupPage_DisappearingAsync;
            await PopupNavigation.Instance.PushAsync(popupPage);
        }

        private async void PopupPage_DisappearingAsync(object sender, EventArgs e)
        {
            SelectedPosition = ((sender as PopupTransferEquipment).BindingContext as PopupTransferEquipmentViewModel).Position;
            var userid = CrossSettings.Current.GetValueOrDefault("UserID", null);
            int.TryParse(userid, out _userID);
            await _ctrl.TransferEquipments(ScannedEquipments.ToList(), _userID, SelectedPosition);
        }

        private async void Scanning()
        {
            IsAnalyzing = false;
            if (ScannedEquipments?.Where(e => e.ISNumber == Result.Text).Count() == 0)
            {

                var addedEquipment = await _ctrl.GetEquipment(Result?.Text);
                foreach (var eq in addedEquipment)
                    eq.Model = await _ctrl.GetModel(eq.IDModel);
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
                    Navigation.PushAsync(new OpenEquipmentPage(_selectedEquipment));
            }
        }
        public string SelectedPosition { get; set; }
        public ObservableCollection<Equipment> ScannedEquipments { get; set; } = new ObservableCollection<Equipment>();
        public bool IsTorchOn { get; set; }
        public bool IsAnalyzing { get; set; } = true;
        public bool IsScanning { get; set; }
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private int _userID;
    }
}