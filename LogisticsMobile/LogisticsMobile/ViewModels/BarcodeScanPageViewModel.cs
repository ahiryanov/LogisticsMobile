using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    public class BarcodeScanPageViewModel :INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        private ServerController _ctrl = new ServerController();

        public ICommand QRScanResultCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsAnalyzing = false;
                    List<Equipment> searchedList;
                    searchedList = await _ctrl.GetEquipment(Result.Text);
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        switch(searchedList?.Count)
                        {
                            case 1:
                                Vibration.Vibrate(TimeSpan.FromMilliseconds(50));
                                var equipmentPage = new OpenEquipmentPage(searchedList[0]);
                                equipmentPage.Disappearing += EquipmentPage_Disappearing;
                                await Navigation.PushAsync(equipmentPage);
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
                    });
                });
            }
        }

        private void EquipmentPage_Disappearing(object sender, EventArgs e)
        {
            IsAnalyzing = true;
        }

        public bool IsTorchOn { get; set; }
        public bool IsAnalyzing { get; set; } = true;
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}