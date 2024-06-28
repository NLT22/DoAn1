using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project1.ViewModel.QLKhoViewModel;
using System.Windows.Input;
using System.Windows;

namespace Project1.ViewModel.KhoViewModel
{
    public class InforMedicineVM: BaseViewModel
    {
        public string Name_TB { get => _name_TB; set { _name_TB = value; OnPropertyChanged(); } }
        private string _name_TB;

        public string Supplier_TB { get => _Supplier_TB; set { _Supplier_TB = value; OnPropertyChanged(); } }
        private string _Supplier_TB;

        public string Price_TB { get => _Price_TB; set { _Price_TB = value; OnPropertyChanged(); } }
        private string _Price_TB;


        public string Decription_TB { get => _Decription_TB; set { _Decription_TB = value; OnPropertyChanged(); } }
        private string _Decription_TB;

        public Unit UnitSelectedItem { get => _unitSelectedItem; set { _unitSelectedItem = value; OnPropertyChanged(); } }
        private Unit _unitSelectedItem;

        public ObservableCollection<Unit> UnitList { get => _UnitList; set { _UnitList = value; OnPropertyChanged(); } }
        private ObservableCollection<Unit> _UnitList;

        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public InforMedicineVM()
        {
            UnitList = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units);
            UpdateInfo();

            SaveCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { SaveNV(); CloseWindow(p); });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) =>
            {
                CloseWindow(p);
            });
        }
        void CloseWindow(Window x)
        {
            var w = Window.GetWindow(x);
            if (w != null)
            {
                w.Close();
            }
        }
        void UpdateInfo()
        {
            var Medicine = DataProvider.Ins.DB.Medicines.FirstOrDefault(x => x.medicine_id == InforMedicineData.Id);
            if (Medicine != null)
            {
                Name_TB = Medicine.medicine_name;
                Supplier_TB = Medicine.supplier;
                Price_TB = Medicine.price.ToString();
                Decription_TB = Medicine.description;
                UnitSelectedItem = Medicine.Unit;
            }
        }
        void SaveNV()
        {
            var Medicine = DataProvider.Ins.DB.Medicines.FirstOrDefault(x => x.medicine_id == InforMedicineData.Id);
            if (Medicine != null)
            {
                Medicine.medicine_name = Name_TB;
                Medicine.supplier = Supplier_TB;
                Medicine.price = (decimal?)double.Parse(Price_TB);
                Medicine.description = Decription_TB;
                Medicine.Unit = UnitSelectedItem;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thuốc thành công");
                return;
            }

            var NewMedicine = new Medicine
            {
                medicine_name = Name_TB,
                supplier = Supplier_TB,
                price = (decimal?)double.Parse(Price_TB),
                description = Decription_TB,
                Unit = UnitSelectedItem,
                quantity = 0

            };
            DataProvider.Ins.DB.Medicines.Add(NewMedicine);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm thuốc thành công");
        }
    }
}
