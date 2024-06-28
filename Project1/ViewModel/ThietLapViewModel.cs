using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;
using Unit = Project1.Model.Unit;

namespace Project1.ViewModel
{
    public class ThietLapViewModel:BaseViewModel
    {
        public int DongDucTB { get => _DongDucTB; set { _DongDucTB = value; OnPropertyChanged(); } }
        private int _DongDucTB;
        public int PhoiGiongTB { get => _PhoiGiongTB; set { _PhoiGiongTB = value; OnPropertyChanged(); } }
        private int _PhoiGiongTB;
        public int MangThaiTB { get => _MangThaiTB; set { _MangThaiTB = value; OnPropertyChanged(); } }
        private int _MangThaiTB;
        public int ThangTuoiTB { get => _ThangTuoiTB; set { _ThangTuoiTB = value; OnPropertyChanged(); } }
        private int _ThangTuoiTB;
        public decimal PartTimeTB { get => _PartTimeTB; set { _PartTimeTB = value; OnPropertyChanged(); } }
        private decimal _PartTimeTB;
        public decimal FullTimeTB { get => _FullTimeTB; set { _FullTimeTB = value; OnPropertyChanged(); } }
        private decimal _FullTimeTB;
        public string NewUnit_TB { get => _NewUnit_TB; set { _NewUnit_TB = value; OnPropertyChanged(); } }
        private string _NewUnit_TB;
        public ObservableCollection<Unit> Unit_List { get => _Unit_List; set { _Unit_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Unit> _Unit_List;
        public ICommand NewUnitCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveThietLapCommand { get; set; }

        public ThietLapViewModel()
        {
            KhoiTao();
            Unit_List = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units);
            DeleteCommand = new RelayCommand<Unit>(CanChoose, DeleteUnit);
            NewUnitCommand = new RelayCommand<object>((p) => { return NewUnit_TB != null; }, (p) => { NewUnitFunc();  });
            SaveThietLapCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SaveFunc(); });
        }
        public void NewUnitFunc()
        {
            var newUnit = new Unit()
            {
                unit_name = NewUnit_TB
            };
            DataProvider.Ins.DB.Units.Add(newUnit);
            Unit_List.Add(newUnit);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Đã thêm đơn vị mới");
        }
        private bool CanChoose(Unit unit)
        {
            return unit != null;
        }
        private void DeleteUnit(Unit unit)
        {
            DataProvider.Ins.DB.Units.Remove(unit);
            Unit_List.Remove(unit);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Đã xóa unit");
        }
        public void SaveFunc()
        {
            DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 1).event_period = DongDucTB;
            DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 2).event_period= PhoiGiongTB ;
            DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 3).event_period = MangThaiTB;
            DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 8).event_period = ThangTuoiTB;
            DataProvider.Ins.DB.Roles.FirstOrDefault(p => p.role_id == 3).wage = PartTimeTB ;
            DataProvider.Ins.DB.Roles.FirstOrDefault(p => p.role_id == 2).wage = FullTimeTB;
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Lưu thiết lập thành công");
        }
        public void KhoiTao()
        {
            DongDucTB = (int)DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 1).event_period;
            PhoiGiongTB = (int)DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 2).event_period;
            MangThaiTB = (int)DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 3).event_period;
            ThangTuoiTB = (int)DataProvider.Ins.DB.Pig_Events.FirstOrDefault(p => p.pig_event_id == 8).event_period;
            PartTimeTB = (decimal)DataProvider.Ins.DB.Roles.FirstOrDefault(p => p.role_id == 3).wage;
            FullTimeTB = (decimal)DataProvider.Ins.DB.Roles.FirstOrDefault(p => p.role_id == 2).wage;
        }

    }
}
