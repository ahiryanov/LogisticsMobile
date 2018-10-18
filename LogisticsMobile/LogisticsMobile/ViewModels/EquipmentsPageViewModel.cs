using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class EquipmentsPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand RefreshCommand { get; protected set; }
        public ICommand AddEquipmentCommand { protected set; get; }
        public ICommand DeleteEquipmentCommand { protected set; get; }

        ServerController _ctrl = new ServerController();
        
        
        private Model _model;

        public EquipmentsPageViewModel(Model model)
        {
            _model = model;
            RefreshCommand = new Command(LoadEquipments);
            AddEquipmentCommand = new Command(AddEquipment);
            DeleteEquipmentCommand = new Command(DeleteEquipment);
            LoadEquipments();
        }

       

        private async void DeleteEquipment(object obj)
        {
            if (obj != null)
            {
                var returnedObj = await _ctrl.DeleteEquipment((obj as Equipment).IDEquipment);
                if (returnedObj != null)
                {
                    _equipments.Remove(obj as Equipment);
                    DependencyService.Get<IMessage>().LongAlert("Удалено!");
                }
            }
        }

        private async void AddEquipment()
        {
            var newEq = new EquipmentInfoPage(new Equipment() { IDModel = _model.IDModel }, true);
            await Navigation.PushAsync(newEq);
        }

        private async void LoadEquipments()
        {
            IsBusy = false;

            _equipments = new ObservableCollection<Equipment>(await _ctrl.GetEquipments(_model));
            var grouping = _equipments.GroupBy(e => e.PositionState).Select(g => new EquipmentsGrouping<string, Equipment>(g.Key, g));
            Equipments = new ObservableCollection<EquipmentsGrouping<string, Equipment>>(grouping);
           
            IsBusy = false;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ObservableCollection<Equipment> _equipments;
        public ObservableCollection<EquipmentsGrouping<string, Equipment>> Equipments
        {
            get;
            
            set;
            
        }

        public Equipment SelectedEquipment
        {
            set
            {
                if (value != null)
                    Navigation.PushAsync(new OpenEquipmentPage(value));
            }
        }


        public class EquipmentsGrouping<K, T> : ObservableCollection<T>
        {
            public string Name { get; private set; }
            public new int Count { get; private set; }
            public EquipmentsGrouping(string name, IEnumerable<T> items)
            {
                if (!string.IsNullOrWhiteSpace(name))
                    Name = name;
                else
                    Name = "<Без положения>";
                foreach (T item in items)
                {
                    Items.Add(item);
                    Count++;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
