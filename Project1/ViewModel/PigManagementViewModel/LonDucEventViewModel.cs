using LiveCharts.Wpf;
using LiveCharts;
using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project1.ViewModel.QuanLyLonNuoiViewModel;
using System.Windows.Input;
using System.Windows;
using Project1.FeatureWindow.PigWindow;
using static Project1.ViewModel.PigManagementViewModel.EventDecriptionViewModel;

namespace Project1.ViewModel.PigManagementViewModel
{
    public class LonDucEventViewModel : BaseViewModel
    {
        public string Decription_TB { get => _Decription_TB; set { _Decription_TB = value; OnPropertyChanged(); } }
        private string _Decription_TB;

        public Pig PigSelectedItem
        {
            get => _PigSelectedItem;
            set
            {
                _PigSelectedItem = value;
                if (value!=null) Event_List = new ObservableCollection<Event>(DataProvider.Ins.DB.Events.Where(p => p.pig_id == value.pig_id && p.event_type == 8));
                OnPropertyChanged();
            }
        }
        private Pig _PigSelectedItem;

        public ObservableCollection<Pig> Pig_List { get => _Pig_List; set { _Pig_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Pig> _Pig_List;

        public ObservableCollection<Event> Event_List { get => _Event_List; set { _Event_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _Event_List;

        public PigPen PenManagementItem
        {
            get => _PenManagementItem;
            set
            {
                _PenManagementItem = value;
                Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.pen_id == value.pen_id && p.gender=="Đực"));
                Event_List = null;
                OnPropertyChanged();
            }
        }
        private PigPen _PenManagementItem;

        public ObservableCollection<PigPen> PenManagement_List { get => _PenManagement_List; set { _PenManagement_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigPen> _PenManagement_List;


        public ICommand CloseWindowCommand { get; set; }
        public ICommand CollectCommand { get; set; }


        public LonDucEventViewModel()
        {
            var pig = DataProvider.Ins.DB.Pigs.FirstOrDefault(p => p.pig_id == InforPig.Id);

            PenManagement_List = new ObservableCollection<PigPen>(DataProvider.Ins.DB.PigPens);
            PenManagementItem = DataProvider.Ins.DB.PigPens.FirstOrDefault(p => p.pen_id == pig.pen_id);
            PigSelectedItem = DataProvider.Ins.DB.Pigs.FirstOrDefault(p => p.pig_id == pig.pig_id);

            CollectCommand = new RelayCommand<Pig>(CanChoose, SaveEvent);
            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { CloseWindow(p); });
        }

        void CloseWindow(Window x)
        {
            var w = Window.GetWindow(x);
            if (w != null)
            {
                w.Close();
            }
        }
        private bool CanChoose(Pig pig)
        {
            return pig != null;
        }
        void SaveEvent(Pig pig)
        {
            GetDescription();
            if (Decription.IsDecription)
            {
                var newEvent = new Event
                {
                    event_type = 8,
                    pig_id = pig.pig_id,
                    event_done = true,
                    event_date = DateTime.Now,
                    description = Decription.DecriptionContent
                };
                DataProvider.Ins.DB.Events.Add(newEvent);
                DataProvider.Ins.DB.SaveChanges();
                Event_List = new ObservableCollection<Event>(DataProvider.Ins.DB.Events.Where(p => p.pig_id == PigSelectedItem.pig_id && p.event_type == 8));
                MessageBox.Show("Thêm lịch sử khai thác tinh thành công");
            }
        }
        void GetDescription()
        {
            PigEventDescription window = new PigEventDescription() {
                DataContext = new EventDecriptionViewModel()
            };
            window.ShowDialog();
        }
    }
}
