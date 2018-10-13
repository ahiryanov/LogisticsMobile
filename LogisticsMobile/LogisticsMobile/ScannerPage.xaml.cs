using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace LogisticsMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScannerPage : ContentPage
    {
		public ScannerPage ()
		{
			InitializeComponent();
		}

        

        public void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned result", result.Text, "OK");
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            zxing.IsVisible = !zxing.IsVisible;
        }
    }
}