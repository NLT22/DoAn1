using Project1.FeatureWindow;
using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.NhanVienViewModel;
using static Project1.ViewModel.QLKhoViewModel;

namespace Project1.ViewModel.KhoViewModel
{
    public class ImportFoodViewModel: BaseViewModel
    {
        public string Name_TB { get => _name_TB; set { _name_TB = value; OnPropertyChanged(); } }
        private string _name_TB;
        public string Decription_TB { get => _Decription_TB; set { _Decription_TB = value; OnPropertyChanged(); } }
        private string _Decription_TB;
        public string Quantity_TB { get => _Quantity_TB; set { _Quantity_TB = value; OnPropertyChanged(); } }
        private string _Quantity_TB;
        public DateTime? ImportDate_TB { get => _ImportDate_TB; set { _ImportDate_TB = value; OnPropertyChanged(); } }
        private DateTime? _ImportDate_TB;

        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ImportFoodViewModel() {
            UpdateInfo();
            SaveCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { Save(); CloseWindow(p); });

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
            var food = DataProvider.Ins.DB.Foods.FirstOrDefault(x => x.food_id == InforFoodData.Id);
            if (food != null)
            {
                Name_TB = food.food_name;
                ImportDate_TB = DateTime.Now;
            }
        }
        void Save()
        {
            var food = DataProvider.Ins.DB.Foods.FirstOrDefault(x => x.food_id == InforFoodData.Id);
            food.quantity += int.Parse(Quantity_TB);
            var newFoodTransaction = new FoodTransaction
            {
                Food_id = InforFoodData.Id,
                transaction_type = "Nhập",
                transaction_date = ImportDate_TB,
                quantity = int.Parse(Quantity_TB),
                expense = int.Parse(Quantity_TB) * food.price,
                description = Decription_TB,
                pig_pen=null
            };
            DataProvider.Ins.DB.FoodTransactions.Add(newFoodTransaction);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Nhập thức ăn thành công");
        }
    }
}
