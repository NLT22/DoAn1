using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.QLKhoViewModel;

namespace Project1.ViewModel.KhoViewModel
{
    public class ExportFoodViewModel : BaseViewModel
    {
        public string Name_TB { get => _name_TB; set { _name_TB = value; OnPropertyChanged(); } }
        private string _name_TB;
        public string Decription_TB { get => _Decription_TB; set { _Decription_TB = value; OnPropertyChanged(); } }
        private string _Decription_TB;
        public string Quantity_TB { get => _Quantity_TB; set { _Quantity_TB = value; OnPropertyChanged(); } }
        private string _Quantity_TB;
        public DateTime? ExportDate_TB { get => _ExportDate_TB; set { _ExportDate_TB = value; OnPropertyChanged(); } }
        private DateTime? _ExportDate_TB;
        public PigPen PenSelectedItem { get => _PenSelectedItem; set { _PenSelectedItem = value; OnPropertyChanged(); } }
        private PigPen _PenSelectedItem;

        public ObservableCollection<PigPen> PigPen_List { get => _PigPen_List; set { _PigPen_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigPen> _PigPen_List;
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ExportFoodViewModel()
        {
            PigPen_List = new ObservableCollection<PigPen>(DataProvider.Ins.DB.PigPens);

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
                ExportDate_TB = DateTime.Now;
            }
        }
        void Save()
        {
            var food = DataProvider.Ins.DB.Foods.FirstOrDefault(x => x.food_id == InforFoodData.Id);
            if (food.quantity < int.Parse(Quantity_TB))
            {
                MessageBox.Show("Không đủ thức ăn");
            }
            else
            {
                food.quantity -= int.Parse(Quantity_TB);
                var newFoodTransaction = new FoodTransaction
                {
                    Food_id = InforFoodData.Id,
                    transaction_type = "Xuất",
                    transaction_date = ExportDate_TB,
                    quantity = int.Parse(Quantity_TB),
                    description = Decription_TB,
                    expense = 0,
                    pig_pen = PenSelectedItem.pen_id
                };
                DataProvider.Ins.DB.FoodTransactions.Add(newFoodTransaction);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xuất thức ăn thành công");
            }
            
        }
    }
}
