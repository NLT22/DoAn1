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
    public class ExportMedicineViewModel : BaseViewModel
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
        public ExportMedicineViewModel()
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
            var medicine = DataProvider.Ins.DB.Medicines.FirstOrDefault(x => x.medicine_id == InforMedicineData.Id);
            if (medicine != null)
            {
                Name_TB = medicine.medicine_name;
                ExportDate_TB = DateTime.Now;
            }
        }
        void Save()
        {
            var medicine = DataProvider.Ins.DB.Medicines.FirstOrDefault(x => x.medicine_id == InforMedicineData.Id);
            if (medicine.quantity < int.Parse(Quantity_TB))
            {
                MessageBox.Show("Không đủ thuốc");
            }
            else
            {
                medicine.quantity -= int.Parse(Quantity_TB);
                var newMedicineTransaction = new MedicineTransaction
                {
                    medicine_id = InforMedicineData.Id,
                    transaction_type = "Xuất",
                    transaction_date = ExportDate_TB,
                    quantity = int.Parse(Quantity_TB),
                    expense = 0,
                    description = Decription_TB,
                    pig_pen = PenSelectedItem.pen_id
                };
                DataProvider.Ins.DB.MedicineTransactions.Add(newMedicineTransaction);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Xuất thuốc thành công");
            }

        }
    }
}
