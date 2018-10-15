using Xamarin.Forms;
using LogisticsMobile.ViewModels;
using System;
using System.Collections.Generic;
using ZXing.Mobile;

namespace LogisticsMobile
{
    public partial class BarCodeScanPage : ContentPage
    {
        public BarCodeScanPage()
        {
            InitializeComponent();
            BindingContext = new BarcodeScanPageViewModel() { Navigation = this.Navigation };
            zxing.Options.CameraResolutionSelector = HandleCameraResolutionSelectorDelegate;
        }

        private void ScannerOverlay_FlashButtonClicked(Button sender, EventArgs e)
        {
            var viewmodel = BindingContext as BarcodeScanPageViewModel;
            viewmodel.IsTorchOn = !viewmodel.IsTorchOn;
            

        }

        private CameraResolution HandleCameraResolutionSelectorDelegate(List<CameraResolution> availableResolutions) //костыль для выбора максимального разрешения камеры
        {
            CameraResolution maxResolution;
            int maxWidth = 0;
            maxResolution = new CameraResolution();
            foreach (var resolution in availableResolutions)
                if (resolution.Width > maxWidth)
                {
                    maxWidth = resolution.Width;
                    maxResolution = resolution;
                }
            return maxResolution;
        }
    }
}