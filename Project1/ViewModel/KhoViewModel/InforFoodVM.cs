using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.NhanVienViewModel;
using static Project1.ViewModel.QLKhoViewModel;

namespace Project1.ViewModel.KhoViewModel
{
    public class InforFoodVM : BaseViewModel
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
        public InforFoodVM()
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
            var Food = DataProvider.Ins.DB.Foods.FirstOrDefault(x => x.food_id == InforFoodData.Id);
            if (Food != null)
            {
                Name_TB = Food.food_name;
                Supplier_TB = Food.supplier;
                Price_TB = Food.price.ToString();
                Decription_TB = Food.description;
                UnitSelectedItem = Food.Unit;
            }
        }
        void SaveNV()
        {
            var Food = DataProvider.Ins.DB.Foods.FirstOrDefault(x => x.food_id == InforFoodData.Id);
            if (Food != null)
            {
                Food.food_name = Name_TB;
                Food.supplier = Supplier_TB;
                Food.price = (decimal?)double.Parse(Price_TB);
                Food.description = Decription_TB;
                Food.Unit = UnitSelectedItem;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thức ăn thành công");
                return;
            }

            var NewFood = new Food
            {
                food_name = Name_TB,
                supplier = Supplier_TB,
                price = (decimal?)double.Parse(Price_TB),
                description = Decription_TB,
                Unit = UnitSelectedItem,
                quantity = 0
            };
            DataProvider.Ins.DB.Foods.Add(NewFood);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm thuốc thành công");
        }
    }
}
