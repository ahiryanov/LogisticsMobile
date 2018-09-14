using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace LogisticsMobile.ViewModels
{
    class EquipmentsPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation;
        public ICommand RefreshPullCommand { get; }
        public ICommand AddEquipmentCommand { protected set; get; }
        public ICommand DeleteEquipmentCommand { protected set; get; }

        ServerController _ctrl = new ServerController();
        private bool _isBusy;
        private ObservableCollection<Equipment> _equipments;
        private Model _model;

        public EquipmentsPageViewModel(Model model)
        {
            _model = model;
            RefreshPullCommand = new Command(LoadEquipments);
            AddEquipmentCommand = new Command(AddEquipment);
            DeleteEquipmentCommand = new Command(DeleteEquipment);
            LoadEquipments();


            MessagingCenter.Subscribe<EquipmentInfoPageViewModel,string>(this, "EquipmentsInfoPage", (sender, arg) => {
                // do something whenever the "Hi" message is sent
                // using the 'arg' parameter which is a string
                switch (arg)
                {
                    case "SaveExist":
                        ShowMessage("Успешно сохранено!", Color.Green);
                        break;
                    case "SaveError":
                        ShowMessage("Ошибка сохранения!", Color.Red);
                        break;
                };
                
            });
        }

        private async void DeleteEquipment(object obj)
        {
            var returnedObj = await _ctrl.DeleteEquipment((obj as Equipment).IDEquipment);
            if (returnedObj != null)
            {
                Equipments.Remove(obj as Equipment);
                ShowMessage("Удалено!", Color.Green);
            }
        }

        private async void AddEquipment()
        {
            var newEq = new EquipmentInfoPage(new Equipment() { IDModel = _model.IDModel }, true);
            await Navigation.PushAsync(newEq);
            var viewModelInfoPage = newEq.BindingContext as EquipmentInfoPageViewModel;
            viewModelInfoPage.SaveNewSuccess += (s, e) => 
            {
                Equipments.Add(viewModelInfoPage.Equipment);
                ShowMessage("Успешно сохранено!", Color.Green);
            };

        }

        private async void LoadEquipments()
        {
            IsBusy = true;
            Equipments = new ObservableCollection<Equipment>(await _ctrl.GetEquipments(_model));
            IsBusy = false;
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ObservableCollection<Equipment> Equipments
        {
            get { return _equipments; }
            set
            {
                _equipments = value;
                OnPropertyChanged(nameof(Equipments));
            }
        }

        public Equipment SelectedEquipment
        {
            set
            {
                if (value != null)
                    Navigation.PushAsync(new OpenEquipmentPage(value));
            }
        }


        private async void ShowMessage(string message, Color color)
        {
            MessageText = message;
            MessageColor = color;
            IsShowMessage = true;
            await Task.Delay(3000);
            IsShowMessage = false;
        }

        private Color _messageColor;
        public Color MessageColor
        {
            get { return _messageColor; }
            set
            {
                if (_messageColor != value)
                {
                    _messageColor = value;
                    OnPropertyChanged(nameof(MessageColor));
                }
            }
        }

        private string _messageText;
        public string MessageText
        {
            get { return _messageText; }
            set
            {
                if (_messageText != value)
                {
                    _messageText = value;
                    OnPropertyChanged(nameof(MessageText));
                }
            }
        }

        private bool _isShowMessage;
        public bool IsShowMessage
        {
            get { return _isShowMessage; }
            set
            {
                if (_isShowMessage != value)
                {
                    _isShowMessage = value;
                    OnPropertyChanged(nameof(IsShowMessage));
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
