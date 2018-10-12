﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    public class BarcodeScanPageViewModel :INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        
        // public ICommand ScanResultCommand { get; protected set; }
        //public ICommand FlashCommand { get; set; }
        public BarcodeScanPageViewModel()
        {
            //ScanResultCommand = new Command(QRScanResultCommand);
        }


        public Command QRScanResultCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsAnalyzing = false;
                    IsScanning = false;

                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // Barcode = Result.Text;
                        
                        await Application.Current.MainPage.DisplayAlert("Alert", Result.Text, "OK");
                        IsTorchOn = !IsTorchOn;
                    });

                    IsAnalyzing = true;
                    IsScanning = true;
                });
            }
        }
        /*
        private async void ScanResult(object obj)
        {
            ZXing.Result result = obj as ZXing.Result;
            IsAnalyzing = false;
            await Application.Current.MainPage.DisplayAlert("Alert", result.Text, "OK");
            IsAnalyzing = true;
        }*/
        public bool IsTorchOn { get; set; }
        
        public bool IsScanning { get; set; } = true;

        public bool IsAnalyzing { get; set; } = true;
        public ZXing.Result Result { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}