using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
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
                        if (searchedList?.Count == 1)
                        {
                            var equipmentPage = new EquipmentInfoPage(searchedList[0], false);
                            equipmentPage.Disappearing += EquipmentPage_Disappearing;
                            await Navigation.PushAsync(equipmentPage);
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
        public bool IsScanning { get; set; } = true;
        public bool IsAnalyzing { get; set; } = true;
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}