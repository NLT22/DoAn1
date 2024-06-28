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
    public class LonCaiEventViewModel : BaseViewModel
    {
        private Visibility _DongDucVisibility;
        public Visibility DongDucVisibility
        {
            get => _DongDucVisibility;
            set
            {
                _DongDucVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _PhoiGiongVisibility;
        public Visibility PhoiGiongVisibility
        {
            get => _PhoiGiongVisibility;
            set
            {
                _PhoiGiongVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _MangThaiVisibility;
        public Visibility MangThaiVisibility
        {
            get => _MangThaiVisibility;
            set
            {
                _MangThaiVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool _IsPhoiGiongChecked;
        public bool IsPhoiGiongChecked
        {
            get { return _IsPhoiGiongChecked; }
            set
            {
                if (_IsPhoiGiongChecked != value)
                {
                    _IsPhoiGiongChecked = value;
                    OnPropertyChanged();
                    if (_IsPhoiGiongChecked)
                    {
                        IsDongDucChecked = false;
                        IsMangThaiChecked = false;
                        HeaderText = "Quản lý phối giống";
                        UpdateVisibility();

                    }
                }
            }
        }

        private bool _IsDongDucChecked;
        public bool IsDongDucChecked
        {
            get { return _IsDongDucChecked; }
            set
            {
                if (_IsDongDucChecked != value)
                {
                    _IsDongDucChecked = value;
                    OnPropertyChanged();
                    if (_IsDongDucChecked)
                    {
                        IsMangThaiChecked = false;
                        IsPhoiGiongChecked = false;
                        HeaderText = "Quản lý động dục";
                        UpdateVisibility();

                    }
                }
            }
        }


        private bool _isMangThaiChecked;
        public bool IsMangThaiChecked
        {
            get { return _isMangThaiChecked; }
            set
            {
                if (_isMangThaiChecked != value)
                {
                    _isMangThaiChecked = value;
                    OnPropertyChanged();
                    if (_isMangThaiChecked)
                    {
                        IsDongDucChecked = false;
                        IsPhoiGiongChecked = false;
                        HeaderText = "Quản lý mang thai";
                        UpdateVisibility();
                    }
                }
            }
        }

        public DateTime? EventDate_TB { get => _EventDate_TB; set { _EventDate_TB = value; OnPropertyChanged(); } }
        private DateTime? _EventDate_TB;

        public string Decription_TB { get => _Decription_TB; set { _Decription_TB = value; OnPropertyChanged(); } }
        private string _Decription_TB;
        public string HeaderText { get => _HeaderText; set { _HeaderText = value; OnPropertyChanged(); } }
        private string _HeaderText;

        public Event PigDongDucItem
        {
            get => _PigDongDucItem;
            set
            {
                _PigDongDucItem = value;
                if (value != null)
                {
                    DongDuc_List = new ObservableCollection<Event>(
                            DataProvider.Ins.DB.Events
                            .Where(p => p.event_type == 1
                                && p.Pig.pen_id == PenManagementItem.pen_id
                                && p.pig_id == value.pig_id
                                ));
                }
                OnPropertyChanged();
            }
        }
        private Event _PigDongDucItem;

        public Event PigPhoiGiongItem
        {
            get => _PigPhoiGiongItem;
            set
            {
                _PigPhoiGiongItem = value;
                if (value != null)
                {
                    PhoiGiong_List = new ObservableCollection<Event>(
                            DataProvider.Ins.DB.Events
                            .Where(p => p.event_type == 2
                                && p.Pig.pen_id == PenManagementItem.pen_id
                                && p.pig_id == value.pig_id
                                ));
                }
                OnPropertyChanged();
            }
        }
        private Event _PigPhoiGiongItem;
        public Event PigMangThaiItem
        {
            get => _PigMangThaiItem;
            set
            {
                _PigMangThaiItem = value;
                if (value != null)
                {
                    MangThai_List = new ObservableCollection<Event>(
                            DataProvider.Ins.DB.Events
                            .Where(p => (p.event_type == 3 || p.event_type == 4 || p.event_type == 5)
                                && p.Pig.pen_id == PenManagementItem.pen_id
                                && p.pig_id == value.pig_id
                                ));
                }
                OnPropertyChanged();
            }
        }
        private Event _PigMangThaiItem;

        public ObservableCollection<Event> PigMangThai_List { get => _PigMangThai_List; set { _PigMangThai_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _PigMangThai_List;

        public ObservableCollection<Event> MangThai_List { get => _MangThai_List; set { _MangThai_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _MangThai_List;
        public ObservableCollection<Event> PigPhoiGiong_List { get => _PigPhoiGiong_List; set { _PigPhoiGiong_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _PigPhoiGiong_List;

        public ObservableCollection<Event> PhoiGiong_List { get => _PhoiGiong_List; set { _PhoiGiong_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _PhoiGiong_List;

        public ObservableCollection<Event> PigDongDuc_List { get => _PigDongDuc_List; set { _PigDongDuc_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _PigDongDuc_List;

        public ObservableCollection<Event> DongDuc_List { get => _DongDuc_List; set { _DongDuc_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Event> _DongDuc_List;
        public ObservableCollection<Pig> Pig_List { get => _Pig_List; set { _Pig_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Pig> _Pig_List;
        public Pig PigSelectedItem
        {
            get => _PigSelectedItem;
            set
            {
                _PigSelectedItem = value;
                OnPropertyChanged();
            }
        }
        private Pig _PigSelectedItem;
        public PigPen PenManagementItem
        {
            get => _PenManagementItem;
            set
            {
                _PenManagementItem = value;
                Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.pen_id == value.pen_id));
                UpdateList();
                OnPropertyChanged();
            }
        }
        private PigPen _PenManagementItem;

        public ObservableCollection<PigPen> PenManagement_List { get => _PenManagement_List; set { _PenManagement_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigPen> _PenManagement_List;


        public ICommand CloseWindowCommand { get; set; }
        public ICommand NewEventCommand { get; set; }
        public ICommand PhoiGiongCommand { get; set; }
        public ICommand MangThaiCommand { get; set; }
        public ICommand DeConCommand { get; set; }
        public ICommand SayThaiCommand { get; set; }
        public ICommand KhongThaiCommand { get; set; }


        public LonCaiEventViewModel()
        {
            var pig = DataProvider.Ins.DB.Pigs.FirstOrDefault(p => p.pig_id == InforPig.Id);
            PenManagement_List = new ObservableCollection<PigPen>(DataProvider.Ins.DB.PigPens);
            PenManagementItem = DataProvider.Ins.DB.PigPens.FirstOrDefault(p => p.pen_id == pig.pen_id);
            IsDongDucChecked = true;
            PigSelectedItem = pig;
            EventDate_TB = DateTime.Now;

            PhoiGiongCommand = new RelayCommand<Event>(CanChoose, SavePhoiGiongEvent);
            MangThaiCommand = new RelayCommand<Event>(CanChoose, SaveMangThaiEvent);
            DeConCommand = new RelayCommand<Event>(CanChoose, SaveDeConEvent);
            KhongThaiCommand = new RelayCommand<Event>(CanChoose, SaveKhongThaiEvent);
            SayThaiCommand = new RelayCommand<Event>(CanChoose, SaveSayThaiEvent);

            NewEventCommand = new RelayCommand<Window>((p) => { return PigSelectedItem != null && Decription_TB != null && EventDate_TB != null; }, (p) => { NewEvent(); });
            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { CloseWindow(p); });
        }


        int? ThoiGianDocDuc = DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 1).event_period;
        int? ThoiGianPhoiGiong = DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 2).event_period;
        int? ThoiGianMangThai = DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 3).event_period;

        void GetDescription()
        {
            PigEventDescription window = new PigEventDescription()
            {
                DataContext = new EventDecriptionViewModel()
            };
            window.ShowDialog();
        }

        //Quản lý các command
        private bool CanChoose(Event e)
        {
            return e != null;
        }
        void SavePhoiGiongEvent(Event e)
        {
            GetDescription();
            if (Decription.IsDecription)
            {
                e.event_done = true;
                var newEvent = new Event
                {
                    event_type = 2,
                    pig_id = e.pig_id,
                    event_done = false,
                    event_date = DateTime.Now,
                    description = Decription.DecriptionContent,
                    event_next_date = EventDate_TB.Value.AddDays((double)ThoiGianPhoiGiong),
                };
                DataProvider.Ins.DB.Events.Add(newEvent);
                DataProvider.Ins.DB.SaveChanges();
                UpdateList();
                MessageBox.Show("Thêm lịch sử phối giống thành công");
            }
        }
        void SaveMangThaiEvent(Event e)
        {
            GetDescription();
            if (Decription.IsDecription)
            {
                e.event_done = true;
                var newEvent = new Event
                {
                    event_type = 3,
                    pig_id = e.pig_id,
                    event_done = false,
                    event_date = DateTime.Now,
                    description = Decription.DecriptionContent,
                    event_next_date = EventDate_TB.Value.AddDays((double)ThoiGianMangThai),
                };
                DataProvider.Ins.DB.Events.Add(newEvent);
                DataProvider.Ins.DB.SaveChanges();
                UpdateList();
                MessageBox.Show("Thêm lịch sử mang thai thành công");
            }
        }
        void SaveDeConEvent(Event e)
        {
            GetDescription();
            if (Decription.IsDecription)
            {
                e.event_done = true;
                var newEvent = new Event
                {
                    event_type = 4,
                    pig_id = e.pig_id,
                    event_done = false,
                    event_date = DateTime.Now,
                    description = Decription.DecriptionContent
                };
                var newEvent1 = new Event
                {
                    event_type = 1,
                    pig_id = e.pig_id,
                    event_done = false,
                    event_date = DateTime.Now.AddDays(5),
                    event_next_date = DateTime.Now.AddDays((double)(ThoiGianDocDuc + 5)),
                    description = Decription.DecriptionContent
                };
                DataProvider.Ins.DB.Events.Add(newEvent);
                DataProvider.Ins.DB.Events.Add(newEvent1);
                DataProvider.Ins.DB.SaveChanges();
                UpdateList();
                MessageBox.Show("Thêm lịch sử đẻ con thành công");
            }
        }
        void SaveKhongThaiEvent(Event e)
        {
            GetDescription();
            if (Decription.IsDecription)
            {
                e.event_done = true;
                var newEvent = new Event
                {
                    event_type = 1,
                    pig_id = e.pig_id,
                    event_done = false,
                    event_date = DateTime.Now,
                    description = Decription.DecriptionContent,
                    event_next_date = DateTime.Now.AddDays((double)ThoiGianDocDuc),
                };
                DataProvider.Ins.DB.Events.Add(newEvent);
                DataProvider.Ins.DB.SaveChanges();
                UpdateList();
                MessageBox.Show("Thêm lịch sử không thai thành công");
            }
        }
        void SaveSayThaiEvent(Event e)
        {
            GetDescription();
            if (Decription.IsDecription)
            {
                e.event_done = true;
                var newEvent = new Event
                {
                    event_type = 1,
                    pig_id = e.pig_id,
                    event_done = false,
                    event_date = DateTime.Now,
                    description = Decription.DecriptionContent
                };
                DataProvider.Ins.DB.Events.Add(newEvent);
                DataProvider.Ins.DB.SaveChanges();
                UpdateList();
                MessageBox.Show("Thêm lịch sử sảy thai thành công");
            }
        }

        void NewEvent()
        {
            var newEvent = new Event { };

            if (IsDongDucChecked)
            {
                newEvent = new Event
                {
                    event_date = EventDate_TB,
                    description = Decription_TB,
                    event_type = 1, //Sự kiện động dục
                    pig_id = PigSelectedItem.pig_id,
                    event_done = false,
                    event_next_date = EventDate_TB.Value.AddDays((double)ThoiGianDocDuc),
                };
            }
            if (IsPhoiGiongChecked)
            {
                newEvent = new Event
                {
                    event_date = EventDate_TB,
                    description = Decription_TB,
                    event_type = 2,
                    pig_id = PigSelectedItem.pig_id,
                    event_done = false,
                    event_next_date = EventDate_TB.Value.AddDays((double)ThoiGianPhoiGiong),
                };
            }
            if (IsMangThaiChecked)
            {
                newEvent = new Event
                {
                    event_date = EventDate_TB,
                    description = Decription_TB,
                    event_type = 3,
                    pig_id = PigSelectedItem.pig_id,
                    event_done = false,
                    event_next_date = EventDate_TB.Value.AddDays((double)ThoiGianMangThai),
                };
            }
            DataProvider.Ins.DB.Events.Add(newEvent);
            DataProvider.Ins.DB.SaveChanges();
            UpdateList();
            MessageBox.Show("Thêm sự kiện thành công");
        }
        void CloseWindow(Window x)
        {
            var w = Window.GetWindow(x);
            if (w != null)
            {
                w.Close();
            }
        }
        private void UpdateVisibility()
        {
            MangThaiVisibility = IsMangThaiChecked ? Visibility.Visible : Visibility.Collapsed;
            DongDucVisibility = IsDongDucChecked ? Visibility.Visible : Visibility.Collapsed;
            PhoiGiongVisibility = IsPhoiGiongChecked ? Visibility.Visible : Visibility.Collapsed;
            UpdateList();
        }
        private void UpdateList()
        {
            if (IsDongDucChecked)
            {
                PigDongDuc_List = new ObservableCollection<Event>(DataProvider.Ins.DB.Events.Where(p => p.event_type == 1 && p.event_done == false && p.Pig.pen_id == PenManagementItem.pen_id));
            }
            if (IsPhoiGiongChecked)
            {
                PigPhoiGiong_List = new ObservableCollection<Event>(DataProvider.Ins.DB.Events.Where(p => p.event_type == 2 && p.event_done == false && p.Pig.pen_id == PenManagementItem.pen_id));
            }
            if (IsMangThaiChecked)
            {
                PigMangThai_List = new ObservableCollection<Event>(DataProvider.Ins.DB.Events.Where(p => p.event_type == 3 && p.event_done == false && p.Pig.pen_id == PenManagementItem.pen_id));
            }
        }
    }
}