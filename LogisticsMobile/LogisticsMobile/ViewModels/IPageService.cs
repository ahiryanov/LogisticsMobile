using System.Threading.Tasks;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    interface IPageService
    {
        Task PushAsync(Page page);
        Task PopAsync();
        Task DisplayAlert(string title, string message, string cancel);
    }
}
