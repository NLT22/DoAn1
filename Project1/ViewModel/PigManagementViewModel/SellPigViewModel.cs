using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.QuanLyLonNuoiViewModel;

namespace Project1.ViewModel.PigManagementViewModel
{
    public class SellPigViewModel : BaseViewModel
    {
        public static bool IsSellPig { get; set; }
        public string Description_TB { get => _Description_TB; set { _Description_TB = value; OnPropertyChanged(); } }
        private string _Description_TB;
        public decimal Price_TB { get => _Price_TB; set { _Price_TB = value; OnPropertyChanged(); } }
        private decimal _Price_TB;
        public DateTime SellDate_TB { get => _SellDate_TB; set { _SellDate_TB = value; OnPropertyChanged(); } }
        private DateTime _SellDate_TB;
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveTaskCommand { get; set; }
        public SellPigViewModel()
        {
            IsSellPig = false;
            SellDate_TB = DateTime.Now;

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { CloseWindow(p); });

            SaveTaskCommand = new RelayCommand<Window>(
                (p) => { return Description_TB != null && Price_TB != 0 && SellDate_TB != null; },
                (p) =>
                {
                    var pig = DataProvider.Ins.DB.Pigs.FirstOrDefault(m => m.pig_id == InforPig.Id);
                    var newExpense = new PenExpense
                    {
                        expense = Price_TB,
                        expense_description = Description_TB,
                        expense_date = SellDate_TB,
                        pig_sell = true,
                        pen_id = pig.pen_id
                    };
                    DataProvider.Ins.DB.PenExpenses.Add(newExpense);
                    pig.pen_id = 1;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Bán lợn thành công");
                    IsSellPig = true;
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
    }
}
