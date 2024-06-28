using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project1.ViewModel.QLKhoViewModel;
using System.Windows.Input;
using System.Windows;

namespace Project1.ViewModel.KhoViewModel
{
    public class ImportMedicineViewModel: BaseViewModel
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
        public ImportMedicineViewModel()
        {
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
            var medicine = DataProvider.Ins.DB.Medicines.FirstOrDefault(x => x.medicine_id == InforMedicineData.Id);
            if (medicine != null)
            {
                Name_TB = medicine.medicine_name;
                ImportDate_TB = DateTime.Now;
            }
        }
        void Save()
        {
            var medicine = DataProvider.Ins.DB.Medicines.FirstOrDefault(x => x.medicine_id == InforMedicineData.Id);
            medicine.quantity += int.Parse(Quantity_TB);
            var newMedicineTransaction = new MedicineTransaction
            {
                medicine_id = InforMedicineData.Id,
                transaction_type = "Nhập",
                transaction_date = ImportDate_TB,
                quantity = int.Parse(Quantity_TB),
                expense = int.Parse(Quantity_TB) * medicine.price,
                description = Decription_TB,
                pig_pen = null
            };
            DataProvider.Ins.DB.MedicineTransactions.Add(newMedicineTransaction);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Nhập thuốc thành công");
        }
    }
}
