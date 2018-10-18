using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;
using LogisticsMobile.ViewModels;
using ZXing.Mobile;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MultiScannerPage : ContentPage
    {
		public MultiScannerPage ()
		{
			InitializeComponent();
            BindingContext = new MultiScannerPageViewModel() { Navigation = this.Navigation };
            zxing.Options.CameraResolutionSelector = HandleCameraResolutionSelectorDelegate;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (sender as ListView).SelectedItem = null;
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
            //return availableResolutions.FirstOrDefault();
        }

        private void MiltiScannerPage_Appearing(object sender, EventArgs e)
        {
            (((ContentPage)sender).BindingContext as MultiScannerPageViewModel).IsScanning = true;
        }

        private void MiltiScannerPage_Disappearing(object sender, EventArgs e)
        {
            (((ContentPage)sender).BindingContext as MultiScannerPageViewModel).IsScanning = false;
        }
    }
}