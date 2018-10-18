using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace LogisticsMobile.ViewModels
{
    public class MultiScannerPageViewModel :INotifyPropertyChanged
    {
        private ServerController _ctrl = new ServerController();
        public INavigation Navigation { get; set; }
        public ICommand DeleteEquipmentCommand { protected set; get; }
        public ICommand QRScanResultCommand { protected set; get; }

        public MultiScannerPageViewModel()
        {
            DeleteEquipmentCommand = new Command(DeleteEquipment);
            QRScanResultCommand = new Command(Scanning);
            //ScannedEquipments.Add(new Equipment() { PositionState = "sldfkjldfj", ISNumber = "798797979", HealthState = "lkdjfldskjf" });
        }

        private async void Scanning()
        {
            IsAnalyzing = false;
            if (ScannedEquipments?.Where(e => e.ISNumber == Result.Text).Count() == 0)
            {
                Vibration.Vibrate(TimeSpan.FromMilliseconds(50));
                var addedEquipment = await _ctrl.GetEquipment(Result.Text);

                switch (addedEquipment?.Count)
                {
                    case 1:
                        ScannedEquipments.Add(addedEquipment.FirstOrDefault());
                        IsAnalyzing = true;
                        break;
                    case 0:
                        //предупреждение оборудование не найдено
                        IsAnalyzing = true;
                        break;
                    default:
                        //предупреждение много едениц оборудования
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
                Navigation.PushAsync(new OpenEquipmentPage(_selectedEquipment));
            }
        }

        public ObservableCollection<Equipment> ScannedEquipments { get; set; } = new ObservableCollection<Equipment>();
        public bool IsTorchOn { get; set; }
        public bool IsAnalyzing { get; set; } = true;
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}