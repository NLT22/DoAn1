using Newtonsoft.Json.Linq;
using Project1.FeatureWindow.KhoWindow;
using Project1.FeatureWindow.TaskWindow;
using Project1.Model;
using Project1.ViewModel.KhoViewModel;
using Project1.ViewModel.TaskViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using static Project1.ViewModel.LoginViewModel;
using static Project1.ViewModel.QLKhoViewModel;
using Task = Project1.Model.Task;

namespace Project1.ViewModel
{
    public class CongViecViewModel : BaseViewModel
    {
        private int _RecentYear;
        public int RecentYear
        {
            get => _RecentYear;
            set
            {
                if (_RecentYear != value)
                {
                    _RecentYear = value;
                    CheckUpdateDate();
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RecentYearMinus2));
                    OnPropertyChanged(nameof(RecentYearMinus1));
                    OnPropertyChanged(nameof(RecentYearPlus1));
                    OnPropertyChanged(nameof(RecentYearPlus2));
                }
            }
        }
        public int RecentYearMinus2 => RecentYear - 2;
        public int RecentYearMinus1 => RecentYear - 1;
        public int RecentYearPlus1 => RecentYear + 1;
        public int RecentYearPlus2 => RecentYear + 2;

        private int _SelectedMonth;
        public int SelectedMonth
        {
            get => _SelectedMonth;
            set
            {
                if (_SelectedMonth != value)
                {
                    _SelectedMonth = value;
                    CheckUpdateDate();
                    OnPropertyChanged();
                }
            }
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                // Cập nhật RecentYear và SelectedMonth dựa trên SelectedDate mới
                RecentYear = value.Year;
                SelectedMonth = value.Month;
                UpdateVietnameseDate();
                //MessageBox.Show(value.ToString());
                StartTime = new DateTime(_selectedDate.Year, _selectedDate.Month, _selectedDate.Day, StartTime.Hour, StartTime.Minute, 0);
                EndTime = new DateTime(_selectedDate.Year, _selectedDate.Month, _selectedDate.Day, EndTime.Hour, EndTime.Minute, 0);
                LoadTaskShiftList();
                LoadShiftList();
                CongViecSummarize = "Công việc còn lại: " + TaskShiftList.Count().ToString();
                CaLamViecSummarize = "Ca làm việc: " + ShiftList.Count().ToString();
                OnPropertyChanged();
            }
        }

        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (_startTime != value)
                {
                    _startTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, value.Hour, value.Minute, 0);
                    //MessageBox.Show(_startTime.ToString());
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                if (_endTime != value)
                {
                    _endTime = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, value.Hour, value.Minute, 0);
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsTaskManagementCheck;
        public bool IsTaskManagementCheck
        {
            get { return _IsTaskManagementCheck; }
            set
            {
                if (_IsTaskManagementCheck != value)
                {
                    _IsTaskManagementCheck = value;
                    OnPropertyChanged();
                    if (_IsTaskManagementCheck)
                    {
                        IsEmployeeCheck = false;
                        TaskManagementVisibility = Visibility.Visible;
                        EmployeeVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private bool _IsEmployeeCheck;
        public bool IsEmployeeCheck
        {
            get { return _IsEmployeeCheck; }
            set
            {
                if (_IsEmployeeCheck != value)
                {
                    _IsEmployeeCheck = value;
                    OnPropertyChanged();
                    if (_IsEmployeeCheck)
                    {
                        IsTaskManagementCheck = false;
                        TaskManagementVisibility = Visibility.Collapsed;
                        EmployeeVisibility = Visibility.Visible;
                        EmployeeList = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
                    }
                }
            }
        }

        private Visibility _TaskManagementVisibility;
        public Visibility TaskManagementVisibility
        {
            get => _TaskManagementVisibility;
            set
            {
                _TaskManagementVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _EmployeeVisibility;
        public Visibility EmployeeVisibility
        {
            get => _EmployeeVisibility;
            set
            {
                _EmployeeVisibility = value;
                OnPropertyChanged();
            }
        }
        public string CongViecSummarize
        {
            get => _CongViecSummarize;
            set
            {
                if (_CongViecSummarize != value)
                {
                    _CongViecSummarize = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _CongViecSummarize;
        public string CaLamViecSummarize
        {
            get => _CaLamViecSummarize;
            set
            {
                if (_CaLamViecSummarize != value)
                {
                    _CaLamViecSummarize = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _CaLamViecSummarize;
        public Shift ShiftSelectedItem
        {
            get => _ShiftSelectedItem;
            set
            {
                if (_ShiftSelectedItem != value)
                {
                    _ShiftSelectedItem = value;
                    LoadAssignedEmployee();
                    OnPropertyChanged();
                }
            }
        }
        private Shift _ShiftSelectedItem; 
        

        public ICommand RYM2Command { get; set; }
        public ICommand RYM1Command { get; set; }
        public ICommand RYP1Command { get; set; }
        public ICommand RYP2Command { get; set; }
        public ICommand NextYearCommand { get; set; }
        public ICommand PreviousYearCommand { get; set; }
        public ICommand SelectMonthCommand { get; set; }
        public ICommand CalendarSelectedDateChangedCommand { get; set; }
        public ICommand NextDayCommand { get; set; }
        public ICommand PreviousDayCommand { get; set; }
        public ICommand NewTaskShiftCommand { get; set; }
        public ICommand CompletedCommand { get; set; }
        public ICommand DeleteTaskShiftCommand { get; set; }
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand DeleteShiftCommand { get; set; }
        public ICommand AssignCommand { get; set; }
        public ICommand UnAssignCommand { get; set; }


        private ObservableCollection<Task> _TaskList;
        public ObservableCollection<Task> TaskList { get => _TaskList; set { _TaskList = value; OnPropertyChanged(); } }
        public Task TaskSelectedItem
        {
            get => _TaskSelectedItem;
            set
            {
                if (_TaskSelectedItem != value)
                {
                    _TaskSelectedItem = value;
                    OnPropertyChanged();
                }
            }
        }
        private Task _TaskSelectedItem;

        private ObservableCollection<TaskShift> _TaskShiftList;
        public ObservableCollection<TaskShift> TaskShiftList
        {
            get => _TaskShiftList; set { _TaskShiftList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Shift> _ShiftList;
        public ObservableCollection<Shift> ShiftList
        {
            get => _ShiftList; set { _ShiftList = value; OnPropertyChanged(); }
        }
        public ObservableCollection<User> EmployeeList { get => _EmployeeList; set { _EmployeeList = value; OnPropertyChanged(); } }
        private ObservableCollection<User> _EmployeeList;
        public ObservableCollection<EmployeeShift> AsssignedEmployeeList { get => _AsssignedEmployeeList; set { _AsssignedEmployeeList = value; OnPropertyChanged(); } }
        private ObservableCollection<EmployeeShift> _AsssignedEmployeeList;
        public CongViecViewModel()
        {
            IsTaskManagementCheck = true;
            SelectedDate = DateTime.Now;

            RYM2Command = new RelayCommand<object>(CanExecute, ExecuteRYM2Command);
            RYM1Command = new RelayCommand<object>(CanExecute, ExecuteRYM1Command);
            RYP1Command = new RelayCommand<object>(CanExecute, ExecuteRYP1Command);
            RYP2Command = new RelayCommand<object>(CanExecute, ExecuteRYP2Command);

            NextYearCommand = new RelayCommand<object>(CanExecute, ExecuteRYP1Command);
            PreviousYearCommand = new RelayCommand<object>(CanExecute, ExecuteRYM1Command);
            SelectMonthCommand = new RelayCommand<object>(CanExecute, ExecuteSelectMonthCommand);

            NextDayCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SelectedDate = SelectedDate.AddDays(1); });
            PreviousDayCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SelectedDate = SelectedDate.AddDays(-1); });
            NewTaskShiftCommand = new RelayCommand<object>((p) => { return TaskSelectedItem!=null; }, (p) => { NewTaskShift(); });

            CompletedCommand = new RelayCommand<TaskShift>(CanCompleted, CompleteFunc);
            DeleteTaskShiftCommand = new RelayCommand<TaskShift>(CanCompleted, DeleteTaskShiftFunc);
            
            DeleteShiftCommand = new RelayCommand<Shift>(CanDeleteShift, DeleteShiftFunc);

            AddNewTaskCommand = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    NewTaskWindow window = new NewTaskWindow()
                    {
                        DataContext = new NewTaskViewModel()
                    };
                    window.ShowDialog();
                    TaskList = new ObservableCollection<Task>(DataProvider.Ins.DB.Tasks);
                });

            TaskList = new ObservableCollection<Task>(DataProvider.Ins.DB.Tasks);
            LoadTaskShiftList();
            LoadShiftList();

            AssignCommand = new RelayCommand<User>(CanAssign, AssignEmployee);
            UnAssignCommand = new RelayCommand<EmployeeShift>(CanUnAssign, UnAssignEmployee);

        }
        private bool CanUnAssign(EmployeeShift employeeShift)
        {
            return employeeShift != null && ShiftSelectedItem != null;
        }
        private void UnAssignEmployee(EmployeeShift employeeShift)
        {
            DataProvider.Ins.DB.EmployeeShifts.Remove(employeeShift);
            AsssignedEmployeeList.Remove(employeeShift);
            DataProvider.Ins.DB.SaveChanges();
            
        }
        private bool CanAssign(User user)
        {
            return user != null && ShiftSelectedItem!=null;
        }
        private void AssignEmployee(User user)
        {
            var newAssign = DataProvider.Ins.DB.EmployeeShifts.FirstOrDefault(p=>p.shift_id==ShiftSelectedItem.shift_id&& p.assigneduser_id==user.user_id);
            if (newAssign != null)
            {
                MessageBox.Show("Đã phân công!");
            }else
            {
                newAssign = new EmployeeShift
                {
                    assigneduser_id = user.user_id,
                    shift_id = ShiftSelectedItem.shift_id
                };
                DataProvider.Ins.DB.EmployeeShifts.Add(newAssign);
                DataProvider.Ins.DB.SaveChanges();
                LoadAssignedEmployee();
                MessageBox.Show("Phân công thành công!");

            }

        }
        public void LoadAssignedEmployee()
        {
            if (ShiftSelectedItem!=null) AsssignedEmployeeList = new ObservableCollection<EmployeeShift>(DataProvider.Ins.DB.EmployeeShifts.Where(p => p.shift_id == ShiftSelectedItem.shift_id));
            else AsssignedEmployeeList.Clear();
        }
        private bool CanDeleteShift(Shift shift)
        {
            return shift != null;
        }
        private void DeleteShiftFunc(Shift shift)
        {
            var taskshift = DataProvider.Ins.DB.TaskShifts.Where(p=>p.shift_id==shift.shift_id);
            var employeeshift = DataProvider.Ins.DB.EmployeeShifts.Where(p => p.shift_id == shift.shift_id);
            DataProvider.Ins.DB.TaskShifts.RemoveRange(taskshift);
            DataProvider.Ins.DB.EmployeeShifts.RemoveRange(employeeshift);
            DataProvider.Ins.DB.Shifts.Remove(shift);
            DataProvider.Ins.DB.SaveChanges();
            ShiftList.Remove(shift);
            AsssignedEmployeeList.Clear();
            LoadTaskShiftList();
            MessageBox.Show("Đã xóa ca làm việc và các công việc liên quan");
        }

        private bool CanCompleted(TaskShift taskshift)
        {
            return taskshift != null;
        }
        private void CompleteFunc(TaskShift taskshift)
        {
            if (taskshift != null)
            {
                DataProvider.Ins.DB.SaveChanges();
            }
        }
        private void DeleteTaskShiftFunc(TaskShift taskshift)
        {
            if (taskshift != null)
            {
                DataProvider.Ins.DB.TaskShifts.Remove(taskshift);
                DataProvider.Ins.DB.SaveChanges();
                TaskShiftList.Remove(taskshift);
            }
        }
        public void LoadTaskShiftList()
        {
            TaskShiftList = new ObservableCollection<TaskShift>(DataProvider.Ins.DB.TaskShifts
                .Where(p => p.Shift.start_time.Year == SelectedDate.Year &&
                            p.Shift.start_time.Month == SelectedDate.Month &&
                            p.Shift.start_time.Day == SelectedDate.Day &&
                            p.Shift.end_time.Year == SelectedDate.Year &&
                            p.Shift.end_time.Month == SelectedDate.Month &&
                            p.Shift.end_time.Day == SelectedDate.Day)
                );
        }
        public void LoadShiftList()
        {
            ShiftList = new ObservableCollection<Shift>(DataProvider.Ins.DB.Shifts
                .Where(p => p.start_time.Year == SelectedDate.Year &&
                            p.start_time.Month == SelectedDate.Month &&
                            p.start_time.Day == SelectedDate.Day &&
                            p.end_time.Year == SelectedDate.Year &&
                            p.end_time.Month == SelectedDate.Month &&
                            p.end_time.Day == SelectedDate.Day)
                );
        }
        public void NewTaskShift()
        {
            var shift = DataProvider.Ins.DB.Shifts.FirstOrDefault(p => p.start_time == StartTime && p.end_time == EndTime);
            if (shift == null)
            {
                shift = new Shift() { start_time = StartTime, end_time = EndTime };
                DataProvider.Ins.DB.Shifts.Add(shift);
                DataProvider.Ins.DB.SaveChanges();
                shift = DataProvider.Ins.DB.Shifts.FirstOrDefault(p => p.start_time == StartTime && p.end_time == EndTime);
            }

            var taskshift = new TaskShift()
            {
                task_id = TaskSelectedItem.task_id,
                shift_id = shift.shift_id,
                complete = false
            };
            DataProvider.Ins.DB.TaskShifts.Add(taskshift);
            DataProvider.Ins.DB.SaveChanges();
            TaskShiftList.Add(taskshift);
            MessageBox.Show("Thêm công việc thành công");
            LoadShiftList();
        }
        private string _vietnameseMonth;
        public string VietnameseMonth
        {
            get => _vietnameseMonth;
            set
            {
                _vietnameseMonth = value;
                OnPropertyChanged(nameof(VietnameseMonth));
            }
        }

        private string _vietnameseDay;
        public string VietnameseDay
        {
            get => _vietnameseDay;
            set
            {
                _vietnameseDay = value;
                OnPropertyChanged(nameof(VietnameseDay));
            }
        }

        private void UpdateVietnameseDate()
        {
            // Cập nhật tháng và ngày tiếng Việt
            VietnameseMonth = GetVietnameseMonth();
            VietnameseDay = GetVietnameseDay();
        }

        private string GetVietnameseMonth()
        {
            switch (SelectedDate.Month)
            {
                case 1:
                    return "Tháng một";
                case 2:
                    return "Tháng hai";
                case 3:
                    return "Tháng ba";
                case 4:
                    return "Tháng tư";
                case 5:
                    return "Tháng năm";
                case 6:
                    return "Tháng sáu";
                case 7:
                    return "Tháng bảy";
                case 8:
                    return "Tháng tám";
                case 9:
                    return "Tháng chín";
                case 10:
                    return "Tháng mười";
                case 11:
                    return "Tháng mười một";
                case 12:
                    return "Tháng mười hai";
                default:
                    return "";
            }
        }

        private string GetVietnameseDay()
        {
            switch (SelectedDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Thứ hai";
                case DayOfWeek.Tuesday:
                    return "Thứ ba";
                case DayOfWeek.Wednesday:
                    return "Thứ tư";
                case DayOfWeek.Thursday:
                    return "Thứ năm";
                case DayOfWeek.Friday:
                    return "Thứ sáu";
                case DayOfWeek.Saturday:
                    return "Thứ bảy";
                case DayOfWeek.Sunday:
                    return "Chủ nhật";
                default:
                    return "";
            }
        }

        private bool CanExecute(object obj)
        {
            return true;
        }
        private void ExecuteRYM2Command(object obj)
        {
            RecentYear -= 2;
        }
        private void ExecuteRYM1Command(object obj)
        {
            RecentYear -= 1;
        }
        private void ExecuteRYP1Command(object obj)
        {
            RecentYear += 1;
        }
        private void ExecuteRYP2Command(object obj)
        {
            RecentYear += 2;
        }
        private void ExecuteSelectMonthCommand(object obj)
        {
            if (int.TryParse(obj.ToString(), out int month))
            {
                SelectedMonth = month;
            }
        }
        private void CheckUpdateDate()
        {
            if (SelectedMonth != 0 && RecentYear != 0)
            {
                var daysInSelectedMonth = DateTime.DaysInMonth(RecentYear, SelectedMonth);
                var newDate = new DateTime(RecentYear, SelectedMonth, SelectedDate.Day >= 28 ? Math.Min(SelectedDate.Day, daysInSelectedMonth) : SelectedDate.Day);

                if (newDate != SelectedDate)
                {
                    SelectedDate = newDate;
                }
            }
        }
    }
}
