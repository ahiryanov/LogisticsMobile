using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    public class BarcodeScanPageViewModel :INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand ScanResultCommand { get; protected set; }
        public ICommand FlashCommand { get; set; }
        public BarcodeScanPageViewModel()
        {
            ScanResultCommand = new Command(ScanResult);
            IsScanning = true;
            IsAnalyzing = true;
        }

        private async void ScanResult(object obj)
        {
            ZXing.Result result = obj as ZXing.Result;
            IsAnalyzing = false;
            await Application.Current.MainPage.DisplayAlert("Alert", result.Text, "OK");
            IsAnalyzing = true;
        }

        public bool IsScanning { get; set; }

        public bool IsAnalyzing { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}