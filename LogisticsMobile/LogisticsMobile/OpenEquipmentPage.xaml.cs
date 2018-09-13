using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LogisticsMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenEquipmentPage : TabbedPage
    {
        public OpenEquipmentPage(Equipment equipment)
        {
            InitializeComponent ();
            Children.Add(new EquipmentInfoPage(equipment,false));
            Children.Add(new EquipmentHistoryPage(equipment));
        }
    }
}