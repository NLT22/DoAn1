using Project1.FeatureWindow;
using Project1.FeatureWindow.KhoWindow;
using Project1.Model;
using Project1.ViewModel.KhoViewModel;
using Project1.ViewModel.NhanViewManagement;
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

namespace Project1.ViewModel
{
    public class QLKhoViewModel : BaseViewModel
    {
        private string _Sumarize;
        public string Sumarize { get { return _Sumarize; } set { _Sumarize = value; OnPropertyChanged(); } }
        private string addNew;
        public string AddNew { get { return addNew; } set { addNew = value; OnPropertyChanged(); } }

        private Visibility _foodVisibility;
        public Visibility FoodVisibility
        {
            get => _foodVisibility;
            set
            {
                _foodVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _medicineVisibility;
        public Visibility MedicineVisibility
        {
            get => _medicineVisibility;
            set
            {
                _medicineVisibility = value;
                OnPropertyChanged();
            }
        }


        private bool _isFoodChecked;
        public bool IsFoodChecked
        {
            get { return _isFoodChecked; }
            set
            {
                if (_isFoodChecked != value)
                {
                    _isFoodChecked = value;
                    OnPropertyChanged();
                    if (_isFoodChecked)
                    {
                        IsMedicineChecked = false;
                        FoodVisibility = Visibility.Visible;
                        MedicineVisibility = Visibility.Collapsed;
                        AddNew = "Loại thức ăn mới";
                        Sumarize = "Tổng số loại thức ăn: " + Food_List.Count().ToString();
                    }
                }
            }
        }

        private bool _isMedicineChecked;
        public bool IsMedicineChecked
        {
            get { return _isMedicineChecked; }
            set
            {
                if (_isMedicineChecked != value)
                {
                    _isMedicineChecked = value;
                    OnPropertyChanged();
                    if (_isMedicineChecked)
                    {
                        IsFoodChecked = false;
                        FoodVisibility = Visibility.Collapsed;
                        MedicineVisibility = Visibility.Visible;
                        AddNew = "Loại thuốc mới";
                        Sumarize = "Tổng số loại thuốc: " + Medicine_List.Count().ToString();

                    }
                }
            }
        }
        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                SearchCommand.Execute(null);
            }
        }
        

        private ObservableCollection<Medicine> _medicine_List;
        public ObservableCollection<Medicine> Medicine_List { get => _medicine_List; set { _medicine_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Food> _food_List;
        public ObservableCollection<Food> Food_List { get => _food_List; set { _food_List = value; OnPropertyChanged(); } }

        

        public static class InforFoodData
        {
            public static int Id { get; set; }
        }
        public static class InforMedicineData
        {
            public static int Id { get; set; }
        }

        private Food _foodSelectedItem;
        private bool _isOpenInforFood;

        public Food FoodSelectedItem
        {
            get => _foodSelectedItem;
            set
            {
                if (_foodSelectedItem != value)
                {
                    _foodSelectedItem = value;
                    OnPropertyChanged();

                    if (_foodSelectedItem != null && !_isOpenInforFood)
                    {
                        _isOpenInforFood = true; // Đánh dấu là cửa sổ đang mở
                        InforFoodData.Id = FoodSelectedItem.food_id;
                        InforFood inforFood = new InforFood()
                        {
                            DataContext = new InforFoodVM(),
                        };
                        inforFood.Closed += (sender, e) => { _isOpenInforFood = false; }; // Đánh dấu là cửa sổ đã đóng
                        inforFood.ShowDialog();
                        // Refresh the data after closing the window
                        Food_List = new ObservableCollection<Food>(DataProvider.Ins.DB.Foods);
                    }
                }
            }
        }
        private Medicine _medicineSelectedItem;
        private bool _isOpenInforMedicine;

        public Medicine MedicineSelectedItem
        {
            get => _medicineSelectedItem;
            set
            {
                if (_medicineSelectedItem != value)
                {
                    _medicineSelectedItem = value;
                    OnPropertyChanged();

                    if (_medicineSelectedItem != null && !_isOpenInforMedicine)
                    {
                        _isOpenInforMedicine = true; // Đánh dấu là cửa sổ đang mở
                        InforMedicineData.Id = MedicineSelectedItem.medicine_id;
                        InforMedicine inforMedicine = new InforMedicine()
                        {
                            DataContext = new InforMedicineVM(),
                        };
                        inforMedicine.Closed += (sender, e) => { _isOpenInforMedicine = false; }; // Đánh dấu là cửa sổ đã đóng
                        inforMedicine.ShowDialog();
                        // Refresh the data after closing the window
                        Medicine_List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
                    }
                }
            }
        }

        public ICommand SearchCommand { get; set; }
        public ICommand LoadInforFoodCommand { get; set; }
        public ICommand AddNewCommand { get; set; }
        public ICommand DeleteFoodCommand { get; set; }
        public ICommand DeleteMedicineCommand { get; set; }
        public ICommand ImportFoodCommand { get; set; }
        public ICommand ExportFoodCommand { get; set; }
        public ICommand ImportMedicineCommand { get; set; }
        public ICommand ExportMedicineCommand { get; set; }

        public QLKhoViewModel()
        {
            Medicine_List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            Food_List = new ObservableCollection<Food>(DataProvider.Ins.DB.Foods);
            
            
            DeleteFoodCommand = new RelayCommand<Food>(CanDeleteFood, DeleteFood);
            DeleteMedicineCommand = new RelayCommand<Medicine>(CanDeleteMedicine, DeleteMedicine);

            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) => { Search(); });
            AddNewCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OpenAddNewWindow(); });
            ImportFoodCommand = new RelayCommand<Food>(CanImportFood, ImportFoodFunc);
            ExportFoodCommand = new RelayCommand<Food>(CanExportFood, ExportFoodFunc);
            ImportMedicineCommand = new RelayCommand<Medicine>(CanImportMedicine, ImportMedicineFunc);
            ExportMedicineCommand = new RelayCommand<Medicine>(CanExportMedicine, ExportMedicineFunc);


            AddNew = "Loại thuốc mới";
            IsMedicineChecked = true;
            FoodVisibility = Visibility.Collapsed;
            MedicineVisibility = Visibility.Visible;


        }
        private bool CanExportMedicine(Medicine medicine)
        {
            return medicine != null;
        }

        private void ExportMedicineFunc(Medicine medicine)
        {
            if (medicine != null)
            {
                InforMedicineData.Id = medicine.medicine_id;
                ExportMedicine exportMedicine = new ExportMedicine()
                {
                    DataContext = new ExportMedicineViewModel(),
                };
                exportMedicine.ShowDialog();
                Medicine_List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            }
        }
        private bool CanImportMedicine(Medicine medicine)
        {
            return medicine != null;
        }

        private void ImportMedicineFunc(Medicine medicine)
        {
            if (medicine != null)
            {
                InforMedicineData.Id = medicine.medicine_id;
                ImportMedicine importMedicine = new ImportMedicine()
                {
                    DataContext = new ImportMedicineViewModel(),
                };
                importMedicine.ShowDialog();
                Medicine_List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            }
        }
        private bool CanExportFood(Food food)
        {
            return food != null;
        }

        private void ExportFoodFunc(Food food)
        {
            if (food != null)
            {
                InforFoodData.Id = food.food_id;
                ExportFood exportFood = new ExportFood()
                {
                    DataContext = new ExportFoodViewModel(),
                };
                exportFood.ShowDialog();
                Food_List = new ObservableCollection<Food>(DataProvider.Ins.DB.Foods);
            }
        }
        private bool CanImportFood(Food food)
        {
            return food != null;
        }

        private void ImportFoodFunc(Food food)
        {
            if (food != null)
            {
                InforFoodData.Id = food.food_id;
                ImportFood importFood = new ImportFood()
                {
                    DataContext = new ImportFoodViewModel(),
                };
                importFood.ShowDialog();
                Food_List = new ObservableCollection<Food>(DataProvider.Ins.DB.Foods);
            }
        }
        private bool CanDeleteFood(Food food)
        {
            return food != null;
        }

        private void DeleteFood(Food food)
        {
            if (food != null)
            {

                DataProvider.Ins.DB.Foods.Remove(food);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã xóa thức ăn");
                Food_List.Remove(food);
            }
        }
        private bool CanDeleteMedicine(Medicine medicine)
        {
            return medicine != null;
        }

        private void DeleteMedicine(Medicine medicine)
        {
            if (medicine != null)
            {

                DataProvider.Ins.DB.Medicines.Remove(medicine);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Đã xóa thuốc");
                Medicine_List.Remove(medicine);
            }
        }
        void OpenAddNewWindow()
        {
            if(IsMedicineChecked)
            {
                InforMedicineData.Id = 0;
                InforMedicine inforMedicine = new InforMedicine()
                {
                    DataContext = new InforMedicineVM(),
                };
                inforMedicine.ShowDialog();
                Medicine_List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
            }
            if (IsFoodChecked)
            {
                InforFoodData.Id = 0;
                InforFood inforFood = new InforFood()
                {
                    DataContext = new InforFoodVM(),
                };
                inforFood.ShowDialog();
                Food_List = new ObservableCollection<Food>(DataProvider.Ins.DB.Foods);
            }
        }
        void Search()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Medicine_List = new ObservableCollection<Medicine>(DataProvider.Ins.DB.Medicines);
                Food_List = new ObservableCollection<Food>(DataProvider.Ins.DB.Foods);
            }
            else
            {
                if (FoodVisibility == Visibility.Visible)
                {
                    IQueryable<Food> query = DataProvider.Ins.DB.Foods;
                    query = query.Where(db => db.food_name.ToLower().Contains(SearchText.ToLower())
                                        || db.supplier.ToLower().Contains(SearchText.ToLower())
                                        || db.Unit.unit_name.ToLower().Contains(SearchText.ToLower())
                                        || db.price.ToString().ToLower().Contains(SearchText.ToLower())
                                        
                                        );
                    Food_List = new ObservableCollection<Food>(query);
                }
                if (MedicineVisibility == Visibility.Visible)
                {
                    IQueryable<Medicine> query = DataProvider.Ins.DB.Medicines;
                    query = query.Where(db => db.medicine_name.ToLower().Contains(SearchText.ToLower())
                                        || db.supplier.ToLower().Contains(SearchText.ToLower())
                                        || db.Unit.unit_name.ToLower().Contains(SearchText.ToLower())
                                        || db.price.ToString().ToLower().Contains(SearchText.ToLower())

                                        );
                    Medicine_List = new ObservableCollection<Medicine>(query);
                }

            }
        }

    }
}
