using Xamarin.Forms;
using LogisticsMobile.ViewModels;
using System;

namespace LogisticsMobile
{
    public partial class BarCodeScanPage : ContentPage
    {
        public BarCodeScanPage()
        {
            InitializeComponent();
            BindingContext = new BarcodeScanPageViewModel() { Navigation = this.Navigation };
        }

        private void ScannerOverlay_FlashButtonClicked(Button sender, EventArgs e)
        {
            var viewmodel = BindingContext as BarcodeScanPageViewModel;
            viewmodel.IsTorchOn = !viewmodel.IsTorchOn;

            
        }

    }
}