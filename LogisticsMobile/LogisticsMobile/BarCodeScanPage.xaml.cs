using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using LogisticsMobile.ViewModels;

namespace LogisticsMobile
{
    public partial class BarCodeScanPage : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;

        public BarCodeScanPage()
        {
            BarcodeScanPageViewModel viewmodel = new BarcodeScanPageViewModel() { Navigation = this.Navigation };
            BindingContext = viewmodel;
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            zxing.SetBinding(ZXingScannerView.ScanResultCommandProperty, new Binding() { Source = viewmodel, Path = "ScanResultCommand" });

            overlay = new ZXingDefaultOverlay
            {
                BottomText = "Наведите камеру на штрих-код",
                ShowFlashButton = true,
            };
            overlay.FlashButtonClicked += (sender, e) =>
            {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);
            Content = grid;
        }

        //   protected override void OnAppearing()
        //   {
        //       base.OnAppearing();

        //       zxing.IsScanning = true;
        //   }

        //   protected override void OnDisappearing()
        //  {
        //      zxing.IsScanning = false;

        //      base.OnDisappearing();
        //   }
    }
}