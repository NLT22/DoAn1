using Project1.FeatureWindow;
using Project1.FeatureWindow.PigWindow;
using Project1.Model;
using Project1.ViewModel.NhanViewManagement;
using Project1.ViewModel.PigManagementViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using static Project1.ViewModel.NhanVienViewModel;
using static Project1.ViewModel.QuanLyLonNuoiViewModel;
using static Project1.ViewModel.PigManagementViewModel.SellPigViewModel;

namespace Project1.ViewModel
{
    public class QuanLyLonNuoiViewModel : BaseViewModel
    {
        private bool _isLonDucChecked;
        public bool IsLonDucChecked
        {
            get { return _isLonDucChecked; }
            set
            {
                if (_isLonDucChecked != value)
                {
                    _isLonDucChecked = value;
                    OnPropertyChanged();
                    if (_isLonDucChecked)
                    {

                        IsLonNaiChecked = false;
                        IsLonConChecked = false;
                        LoadPigs();
                    }
                }
            }
        }

        private bool _isLonNaiChecked;
        public bool IsLonNaiChecked
        {
            get { return _isLonNaiChecked; }
            set
            {
                if (_isLonNaiChecked != value)
                {
                    _isLonNaiChecked = value;
                    OnPropertyChanged();
                    if (_isLonNaiChecked)
                    {

                        IsLonDucChecked = false;
                        IsLonConChecked = false;
                        LoadPigs();
                    }
                }
            }
        }
        private bool _isLonConChecked;
        public bool IsLonConChecked
        {
            get { return _isLonConChecked; }
            set
            {
                if (_isLonConChecked != value)
                {
                    _isLonConChecked = value;
                    OnPropertyChanged();
                    if (_isLonConChecked)
                    {
                        IsLonNaiChecked = false;
                        IsLonDucChecked = false;
                        LoadPigs();
                    }
                }
            }
        }

        private void LoadPigs()
        {
            if (PenSelectedItem != null)
            {
                if (IsLonDucChecked)
                {
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.gender == "Đực" && p.pen_id == PenSelectedItem.pen_id));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }
                else if (IsLonNaiChecked)
                {
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.gender == "Cái" && p.pen_id == PenSelectedItem.pen_id));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }
                else if (IsLonConChecked)
                {
                    var NgayTuoi = -DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 8).event_period;
                    var Tuoi = DateTime.Now.AddDays((double)NgayTuoi);
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs
                        .Where(p => p.pen_id == PenSelectedItem.pen_id
                        && p.date_of_birth <= Tuoi));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }
                else
                {
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.pen_id == PenSelectedItem.pen_id));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }
            }
            else
            {
                if (IsLonDucChecked)
                {
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.gender == "Đực" && p.pen_id != 1));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }
                else if (IsLonNaiChecked)
                {
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.gender == "Cái" && p.pen_id != 1));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }
                else if (IsLonConChecked)
                {
                    var pigEvent = DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 8);
                    if (pigEvent != null)
                    {
                        var Tuoi = DateTime.Now.AddDays((double)-pigEvent.event_period);
                        Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs
                            .Where(p => p.date_of_birth <= Tuoi && p.pen_id != 1));
                        Sumarize = "Tổng số lợn: " + Pig_List.Count.ToString();
                    }
                }
                else
                {
                    Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs.Where(p => p.pen_id !=1));
                    Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
                }

            }
        }
        private string _Sumarize;
        public string Sumarize { get { return _Sumarize; } set { _Sumarize = value; OnPropertyChanged(); } }

        private string _SearchText;
        public string SearchText { get { return _SearchText; } set { _SearchText = value; OnPropertyChanged(); SearchCommand.Execute(null); } }


        public PigPen PenSelectedItem { get => _PenSelectedItem; set { _PenSelectedItem = value; OnPropertyChanged(); LoadPigs(); } }
        private PigPen _PenSelectedItem;

        public ObservableCollection<PigPen> PigPen_List { get => _PigPen_List; set { _PigPen_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigPen> _PigPen_List;
        public Pig PigSelectedItem { get => _PigSelectedItem; set { _PigSelectedItem = value; OnPropertyChanged(); } }
        private Pig _PigSelectedItem;

        public ObservableCollection<Pig> Pig_List { get => _Pig_List; set { _Pig_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Pig> _Pig_List;
        public static class InforPig
        {
            public static int Id { get; set; }
        }
        public ICommand InforCommand { get; set; }
        public ICommand NhapLonCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EventManagementCommand { get; set; }
        public ICommand SellCommand { get; set; }


        public QuanLyLonNuoiViewModel()
        {
            PigPen_List = new ObservableCollection<PigPen>(DataProvider.Ins.DB.PigPens);
            LoadPigs();

            InforCommand = new RelayCommand<Pig>(CanChoose, ChoosePig);
            NhapLonCommand = new RelayCommand<object>((p) => { return true; }, (p) => { NhapLonMoi(); });
            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SearchPigs(); });
            EventManagementCommand = new RelayCommand<Pig>(CanChoose, PigEventMangement);
            SellCommand = new RelayCommand<Pig>(CanChoose, SellPig);
        }

        public void NhapLonMoi()
        {
            // Tạo bản ghi Pig mới với dữ liệu rỗng
            var pig = new Pig();
            DataProvider.Ins.DB.Pigs.Add(pig);
            DataProvider.Ins.DB.SaveChanges();

            // Lấy Id của bản ghi mới tạo
            InforPig.Id = pig.pig_id;

            // Hiển thị cửa sổ NewPig để người dùng nhập dữ liệu
            NewPig newPigWindow = new NewPig()
            {
                DataContext = new NewPigViewModel(),
            };
            newPigWindow.ShowDialog();

            // Lấy bản ghi Pig mới tạo từ cơ sở dữ liệu
            var expect_pig = DataProvider.Ins.DB.Pigs.SingleOrDefault(q => q.pig_id == pig.pig_id);

            // Kiểm tra các trường dữ liệu, nếu có trường nào rỗng thì xóa bản ghi và các dữ liệu liên quan
            if (expect_pig != null && (expect_pig.pig_type == null || expect_pig.gender == null ||
                                       expect_pig.date_of_arrival == null || expect_pig.date_of_birth == null ||
                                       expect_pig.pen_id == null || expect_pig.origin == null))
            {
                // Xóa các bản ghi liên quan trong bảng Vacine_history
                var vacineHistories = DataProvider.Ins.DB.Vacine_history.Where(v => v.pig_id == expect_pig.pig_id);
                DataProvider.Ins.DB.Vacine_history.RemoveRange(vacineHistories);

                // Xóa các bản ghi liên quan trong bảng PigMeasurements
                var pigMeasurements = DataProvider.Ins.DB.PigMeasurements.Where(m => m.pig_id == expect_pig.pig_id);
                DataProvider.Ins.DB.PigMeasurements.RemoveRange(pigMeasurements);

                // Xóa các bản ghi liên quan trong bảng SickPigs
                var sickPigs = DataProvider.Ins.DB.SickPigs.Where(s => s.pig_id == expect_pig.pig_id);
                DataProvider.Ins.DB.SickPigs.RemoveRange(sickPigs);

                // Xóa bản ghi Pig
                DataProvider.Ins.DB.Pigs.Remove(expect_pig);
                DataProvider.Ins.DB.SaveChanges();
            }
        }
        private bool CanChoose(Pig pig)
        {
            // Xác định logic kiểm tra có thể sửa đổi user hay không
            return pig != null;
        }
        private void PigEventMangement(Pig pig)
        {
            InforPig.Id = pig.pig_id;
            if (pig.gender == "Đực")
            {
                LonDucEvent infoPig = new LonDucEvent()
                {
                    DataContext = new LonDucEventViewModel(),
                };
                infoPig.ShowDialog();
            }
            else
            {
                LonCaiEvent infoPig = new LonCaiEvent()
                {
                    DataContext = new LonCaiEventViewModel(),
                };
                infoPig.ShowDialog();
            }
            LoadPigs();
        }
        private void SellPig(Pig pig)
        {
            InforPig.Id = pig.pig_id;
            SellPigWindow win = new SellPigWindow()
            {
                DataContext = new SellPigViewModel()
            };
            win.ShowDialog();
            if (IsSellPig) Pig_List.Remove(pig);
        }

        private void ChoosePig(Pig pig)
        {
            InforPig.Id = pig.pig_id;
            InforPigWindow infoPig = new InforPigWindow()
            {
                DataContext = new InforPigViewModel(),
            };
            infoPig.ShowDialog();
            LoadPigs();
        }

        void SearchPigs()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                LoadPigs();
            }
            else
            {
                IQueryable<Pig> query = DataProvider.Ins.DB.Pigs;

                // Điều kiện tìm kiếm chung
                query = query.Where(db => db.pig_id.ToString().Contains(SearchText.ToLower())
                                        || db.date_of_arrival.ToString().Contains(SearchText.ToLower())
                                        || db.date_of_birth.ToString().Contains(SearchText.ToLower())
                                        || db.pig_type.ToLower().Contains(SearchText.ToLower())
                                        || db.origin.ToLower().Contains(SearchText.ToLower()));

                // Điều kiện theo pen_id
                if (PenSelectedItem != null)
                {
                    query = query.Where(db => db.pen_id == PenSelectedItem.pen_id);
                }

                // Điều kiện theo giới tính hoặc độ tuổi
                if (IsLonDucChecked)
                {
                    query = query.Where(db => db.gender == "Đực");
                }
                else if (IsLonNaiChecked)
                {
                    query = query.Where(db => db.gender == "Cái");
                }
                else if (IsLonConChecked)
                {
                    query = query.Where(db => db.MonthsOld < 2);
                }

                Pig_List = new ObservableCollection<Pig>(query);
                Sumarize = "Tổng số lợn: " + Pig_List.Count().ToString();
            }
        }

    }
}
