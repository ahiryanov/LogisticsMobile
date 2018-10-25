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
            await PopupNavigation.Instance.PushAsync(new PopupTransferEquipment(await _ctrl.GetPositions()));
        }

        private async void Scanning()
        {
            IsAnalyzing = false;
            if (ScannedEquipments?.Where(e => e.ISNumber == Result.Text).Count() == 0)
            {

                var addedEquipment = await _ctrl.GetEquipment(Result.Text);
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
                        IsAnalyzing = true;
                        break;
                    default:
                        DependencyService.Get<IMessage>().LongAlert("Несколько едениц оборудования с этим ИСН");
                        IsAnalyzing = true;
                        break;
                }
            }
            else
                IsAnalyzing = true;
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

        private string _selectedPosition;
        public string SelectedPosition
        {
            get => _selectedPosition;
            set { _selectedPosition = value; }
        }
        public ObservableCollection<Equipment> ScannedEquipments { get; set; } = new ObservableCollection<Equipment>();
        public bool IsTorchOn { get; set; }
        public bool IsAnalyzing { get; set; } = true;
        public bool IsScanning { get; set; }
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}