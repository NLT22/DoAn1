using LiveCharts;
using LiveCharts.Wpf;
using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Project1.ViewModel
{
    public class TaiChinhViewModel : BaseViewModel
    {
        private Visibility _TongQuanVisibility = Visibility.Collapsed;
        public Visibility TongQuanVisibility
        {
            get => _TongQuanVisibility;
            set
            {
                _TongQuanVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _khoVisibility = Visibility.Collapsed;
        public Visibility KhoVisibility
        {
            get => _khoVisibility;
            set
            {
                _khoVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _nhanVienVisibility = Visibility.Collapsed;
        public Visibility NhanVienVisibility
        {
            get => _nhanVienVisibility;
            set
            {
                _nhanVienVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _chuongLonVisibility = Visibility.Collapsed;
        public Visibility ChuongLonVisibility
        {
            get => _chuongLonVisibility;
            set
            {
                _chuongLonVisibility = value;
                OnPropertyChanged();
            }
        }
        private bool _IsTongQuanChecked;
        public bool IsTongQuanChecked
        {
            get { return _IsTongQuanChecked; }
            set
            {
                if (_IsTongQuanChecked != value)
                {
                    _IsTongQuanChecked = value;
                    OnPropertyChanged();
                    if (_IsTongQuanChecked)
                    {
                        IsKhoChecked = false;
                        IsNhanVienChecked = false;
                        IsChuongLonChecked = false;
                        UpdateVisibility();
                    }
                }
            }
        }

        private bool _IsKhoChecked;
        public bool IsKhoChecked
        {
            get { return _IsKhoChecked; }
            set
            {
                if (_IsKhoChecked != value)
                {
                    _IsKhoChecked = value;
                    OnPropertyChanged();
                    if (_IsKhoChecked)
                    {
                        IsTongQuanChecked = false;
                        IsNhanVienChecked = false;
                        IsChuongLonChecked = false;
                        UpdateVisibility();
                    }
                }
            }
        }

        private bool _IsNhanVienChecked;
        public bool IsNhanVienChecked
        {
            get { return _IsNhanVienChecked; }
            set
            {
                if (_IsNhanVienChecked != value)
                {
                    _IsNhanVienChecked = value;
                    OnPropertyChanged();
                    if (_IsNhanVienChecked)
                    {
                        IsKhoChecked = false;
                        IsTongQuanChecked = false;
                        IsChuongLonChecked = false;
                        UpdateVisibility();
                    }
                }
            }
        }

        private bool _IsChuongLonChecked;
        public bool IsChuongLonChecked
        {
            get { return _IsChuongLonChecked; }
            set
            {
                if (_IsChuongLonChecked != value)
                {
                    _IsChuongLonChecked = value;
                    OnPropertyChanged();
                    if (_IsChuongLonChecked)
                    {
                        IsKhoChecked = false;
                        IsTongQuanChecked = false;
                        IsNhanVienChecked = false;
                        UpdateVisibility();
                    }
                }
            }
        }
        public DateTime SelectedMonth { get => _SelectedMonth; set { _SelectedMonth = value; LoadData(value); OnPropertyChanged(); } }
        private DateTime _SelectedMonth;
        public DateTime OtherCostDate_TB { get => _OtherCostDate_TB; set { _OtherCostDate_TB = value; OnPropertyChanged(); } }
        private DateTime _OtherCostDate_TB;
        public decimal Price_TB { get => _Price_TB; set { _Price_TB = value; OnPropertyChanged(); } }
        private decimal _Price_TB;
        public string Decription_TB { get => _Decription_TB; set { _Decription_TB = value; OnPropertyChanged(); } }
        private string _Decription_TB;
        public PigPen PenSelectedItem { get => _PenSelectedItem; set { _PenSelectedItem = value; OnPropertyChanged(); } }
        private PigPen _PenSelectedItem;
        public ObservableCollection<PigPen> PigPen_List { get => _PigPen_List; set { _PigPen_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigPen> _PigPen_List;
        public ObservableCollection<PenExpense> PenExpense_List { get => _PenExpense_List; set { _PenExpense_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PenExpense> _PenExpense_List;
        public ObservableCollection<FoodTransaction> FoodExpenseList { get => _FoodExpenseList; set { _FoodExpenseList = value; OnPropertyChanged(); } }
        private ObservableCollection<FoodTransaction> _FoodExpenseList;
        public ObservableCollection<Salary_history> SalaryList { get => _SalaryList; set { _SalaryList = value; OnPropertyChanged(); } }
        private ObservableCollection<Salary_history> _SalaryList;
        public ObservableCollection<MedicineTransaction> MedicineExpenseList { get => _MedicineExpenseList; set { _MedicineExpenseList = value; OnPropertyChanged(); } }
        private ObservableCollection<MedicineTransaction> _MedicineExpenseList;

        public SeriesCollection ExpenseDataSeries { get => _ExpenseDataSeries; set { _ExpenseDataSeries = value; OnPropertyChanged(); } }
        private SeriesCollection _ExpenseDataSeries;
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public SeriesCollection MonthsDataSeries { get => _MonthsDataSeries; set { _MonthsDataSeries = value; OnPropertyChanged(); } }
        private SeriesCollection _MonthsDataSeries;
        public string[] MonthLabels { get; set; }
        public Func<double, string> MonthFormatter { get; set; }

        public ICommand RewardCommand { get; set; }
        public ICommand PunishCommand { get; set; }
        public ICommand NewPenExpenseCommand { get; set; }
        public ICommand UpdateDataCommand { get; set; }
        public ICommand DeleteCostCommand { get; set; }

        public TaiChinhViewModel()
        {
            SelectedMonth = DateTime.Now;
            IsTongQuanChecked = true;


            PigPen_List = new ObservableCollection<PigPen>(DataProvider.Ins.DB.PigPens);
            OtherCostDate_TB = DateTime.Now;

            RewardCommand = new RelayCommand<Salary_history>(CanReward, RewardFunc);
            PunishCommand = new RelayCommand<Salary_history>(CanReward, PunishFunc);

            DeleteCostCommand = new RelayCommand<PenExpense>(CanDeleteCost, DeleteCostFunc);
            NewPenExpenseCommand = new RelayCommand<Window>(
                (p) => { return OtherCostDate_TB != null && Price_TB != 0 && PenSelectedItem != null; },
                (p) => { NewPenExpenseFunc(); });
            UpdateDataCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UpdateData(SelectedMonth); });
        }
        private void NewPenExpenseFunc()
        {
            var newExpense = new PenExpense
            {
                expense = Price_TB,
                expense_date = OtherCostDate_TB,
                expense_description = Decription_TB,
                pig_sell = false,
                pen_id = PenSelectedItem.pen_id
            };
            DataProvider.Ins.DB.PenExpenses.Add(newExpense);
            PenExpense_List.Add(newExpense);
            DataProvider.Ins.DB.SaveChanges();
        }
        private bool CanDeleteCost(PenExpense penExpense)
        {
            return penExpense != null;
        }
        private void DeleteCostFunc(PenExpense penExpense)
        {
            PenExpense_List.Remove(penExpense);
            DataProvider.Ins.DB.PenExpenses.Remove(penExpense);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void LoadData(DateTime month)
        {
            var currentMonth = new DateTime(month.Year, month.Month, 1);
            var nextMonth = currentMonth.AddMonths(1);
            var db = DataProvider.Ins.DB;

            FoodExpenseList = new ObservableCollection<FoodTransaction>(db.FoodTransactions.Where(p => p.transaction_date >= currentMonth && p.transaction_date < nextMonth));
            MedicineExpenseList = new ObservableCollection<MedicineTransaction>(db.MedicineTransactions.Where(p => p.transaction_date >= currentMonth && p.transaction_date < nextMonth));
            SalaryList = new ObservableCollection<Salary_history>(db.Salary_history.Where(p => p.month >= currentMonth && p.month < nextMonth));
            PenExpense_List = new ObservableCollection<PenExpense>(db.PenExpenses.Where(p => p.expense_date >= currentMonth && p.expense_date < nextMonth));

            LoadChart(currentMonth);//Tạo chart theo tháng
            LoadMonthlyFinancialData(currentMonth);//Load dữ liệu theo năm
        }
        public void LoadMonthlyFinancialData(DateTime year)
        {
            var currentYear = year.Year;
            var monthlyRecords = DataProvider.Ins.DB.MonthlyFinancialRecords
                .Where(record => record.record_month.Year == currentYear)
                .OrderBy(record => record.record_month)
                .ToList();

            var employeeCosts = new ChartValues<decimal>();
            var foodCosts = new ChartValues<decimal>();
            var medicineCosts = new ChartValues<decimal>();
            var pigSell = new ChartValues<decimal>();
            var otherCosts = new ChartValues<decimal>();

            foreach (var record in monthlyRecords)
            {
                employeeCosts.Add((decimal)record.employee_cost);
                foodCosts.Add((decimal)record.food_cost);
                medicineCosts.Add((decimal)record.medicine_cost);
                pigSell.Add((decimal)record.pig_sell);
                otherCosts.Add((decimal)record.other_cost);
            }

            var expenseDataSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Lương nhân viên",
                    Values = employeeCosts,

                },
                new LineSeries
                {
                    Title = "Thức ăn",
                    Values = foodCosts,

                },
                new LineSeries
                {
                    Title = "Thuốc men",
                    Values = medicineCosts,

                },
                new LineSeries
                {
                    Title = "Lợn bán",
                    Values = pigSell,

                },
                new LineSeries
                {
                    Title = "Chi phí khác",
                    Values = otherCosts,

                }
            };

            var monthLabels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };

            MonthsDataSeries = expenseDataSeries;
            MonthLabels = monthLabels;
            MonthFormatter = value => value.ToString("#,##0") + " VND";
        }

        public void LoadChart(DateTime month)
        {
            var expenseDataSeries = new SeriesCollection();

            // Tạo một SeriesCollection để chứa các ColumnSeries riêng biệt
            var salarySeries = new ColumnSeries
            {
                Title = "Lương",
                Values = new ChartValues<decimal>(),

            };

            var foodCostSeries = new ColumnSeries
            {
                Title = "Thức ăn",
                Values = new ChartValues<decimal>(),
            };

            var medicineCostSeries = new ColumnSeries
            {
                Title = "Thuốc men",
                Values = new ChartValues<decimal>(),
            };

            var pigSellSeries = new ColumnSeries
            {
                Title = "Bán lợn",
                Values = new ChartValues<decimal>(),
            };

            var otherCostSeries = new ColumnSeries
            {
                Title = "Khác",
                Values = new ChartValues<decimal>(),
            };

            var db = DataProvider.Ins.DB;
            var currentMonthRecord = db.MonthlyFinancialRecords.FirstOrDefault(p => p.record_month == month);
            // Tính toán giá trị cho từng cột

            decimal sumEmployeeSalary = (decimal)currentMonthRecord.employee_cost;
            decimal sumFoodCost = (decimal)currentMonthRecord.food_cost;
            decimal sumMedicineCost = (decimal)currentMonthRecord.medicine_cost;
            decimal sumPigSell = (decimal)currentMonthRecord.pig_sell;
            decimal sumOtherCost = (decimal)currentMonthRecord.other_cost;


            var series = new ColumnSeries
            {
                Title = "Chi phí",
                Values = new ChartValues<decimal> { sumEmployeeSalary, sumFoodCost, sumMedicineCost, sumPigSell, sumOtherCost },
                Fill = new SolidColorBrush(Color.FromRgb(0x00, 0x7F, 0x73)),
                DataLabels = true, // Bật DataLabels
                LabelPoint = point => $"{point.Y:#,##0} VND" // Định dạng nhãn dữ liệu như tiền tệ Việt Nam Đồng không có phần thập phân

            };
            expenseDataSeries.Add(series);
            Labels = new[] { "Lương nhân viên", "Thức ăn", "Thuốc men", "Lợn bán", "Chi phí khác" };
            Formatter = value => value.ToString("#,##0") + " VND"; // Định dạng giá trị trên trục Y
            ExpenseDataSeries = expenseDataSeries;

        }

        public void UpdateData(DateTime month)
        {
            var currentMonth = new DateTime(month.Year, month.Month, 1);
            CalculateSalaries(currentMonth);
            
            var db = DataProvider.Ins.DB;

            decimal sumEmployeeSalary = (decimal)SalaryList.Sum(salary => salary.sum_salary);
            decimal sumFoodCost = (decimal)FoodExpenseList.Where(p => p.transaction_type == "Nhập").Sum(ep => ep.expense);
            decimal sumMedicineCost = (decimal)MedicineExpenseList.Where(p => p.transaction_type == "Nhập").Sum(ep => ep.expense);
            decimal sumPigSell = (decimal)PenExpense_List.Where(p => p.pig_sell == true).Sum(ep => ep.expense);
            decimal sumOtherCost = (decimal)PenExpense_List.Where(p => p.pig_sell == false).Sum(ep => ep.expense);

            var existingMonthRecord = db.MonthlyFinancialRecords.FirstOrDefault(sh => sh.record_month == currentMonth);
            if (existingMonthRecord != null)
            {
                existingMonthRecord.employee_cost = sumEmployeeSalary;
                existingMonthRecord.food_cost = sumFoodCost;
                existingMonthRecord.medicine_cost = sumMedicineCost;
                existingMonthRecord.pig_sell = sumPigSell;
                existingMonthRecord.other_cost = sumOtherCost;
            }
            else
            {
                var monthRecord = new MonthlyFinancialRecord
                {
                    employee_cost = sumEmployeeSalary,
                    food_cost = sumFoodCost,
                    medicine_cost = sumMedicineCost,
                    pig_sell = sumPigSell,
                    other_cost = sumOtherCost,
                    record_month = month
                };
                db.MonthlyFinancialRecords.Add(monthRecord);
            }
            db.SaveChanges();
            SelectedMonth = month;
        }

        private bool CanReward(Salary_history salary_History)
        {
            return salary_History != null;
        }
        private void RewardFunc(Salary_history salary_History)
        {
            salary_History.bonus += 100000;
        }
        private void PunishFunc(Salary_history salary_History)
        {
            salary_History.bonus -= 100000;
        }
        private void CalculateSalaries(DateTime month)
        {
            var db = DataProvider.Ins.DB;
            var users = db.Users.ToList();

            var currentMonth = month;
            var nextMonth = currentMonth.AddMonths(1);

            foreach (var user in users)
            {
                var completedShifts = db.EmployeeShifts
                    .Where(es => es.assigneduser_id == user.user_id && es.Shift.start_time >= currentMonth && es.Shift.start_time < nextMonth)
                    .Select(es => es.Shift)
                    .ToList();

                decimal totalHoursWorked = 0;
                foreach (var shift in completedShifts)
                {
                    totalHoursWorked += (decimal)(shift.end_time - shift.start_time).TotalHours;
                }

                var wage = user.Role.wage;
                var salary = totalHoursWorked * wage;

                // Kiểm tra xem bản ghi lương đã tồn tại hay chưa
                var existingSalaryHistory = db.Salary_history.FirstOrDefault(sh => sh.user_id == user.user_id && sh.month == currentMonth);

                if (existingSalaryHistory != null)
                {
                    // Cập nhật bản ghi lương nếu đã tồn tại
                    existingSalaryHistory.sum_salary = salary + existingSalaryHistory.bonus;
                }
                else
                {
                    // Tạo bản ghi lương mới nếu chưa tồn tại
                    var salaryHistory = new Salary_history
                    {
                        user_id = user.user_id,
                        month = currentMonth,
                        bonus = 0,
                        sum_salary = salary
                    };
                    db.Salary_history.Add(salaryHistory);
                }
            }

            db.SaveChanges();

        }
        private void UpdateVisibility()
        {
            TongQuanVisibility = IsTongQuanChecked ? Visibility.Visible : Visibility.Collapsed;
            NhanVienVisibility = IsNhanVienChecked ? Visibility.Visible : Visibility.Collapsed;
            KhoVisibility = IsKhoChecked ? Visibility.Visible : Visibility.Collapsed;
            ChuongLonVisibility = IsChuongLonChecked ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
