using Project1.FeatureWindow;
using Project1.Model;
using Project1.ViewModel.NhanViewManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Project1.ViewModel
{
    public class NhanVienViewModel : BaseViewModel
    {
        private bool _isAllChecked;
        public bool IsAllChecked
        {
            get { return _isAllChecked; }
            set
            {
                if (_isAllChecked != value)
                {
                    _isAllChecked = value;
                    OnPropertyChanged();
                    if (_isAllChecked)
                    {
                        IsFullTimeChecked = false;
                        IsPartTimeChecked = false;
                        List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
                        StaffSumarize = "Tổng số nhân viên: " + List.Count().ToString();
                    }
                }
            }
        }

        private bool _isFullTimeChecked;
        public bool IsFullTimeChecked
        {
            get { return _isFullTimeChecked; }
            set
            {
                if (_isFullTimeChecked != value)
                {
                    _isFullTimeChecked = value;
                    OnPropertyChanged();
                    if (_isFullTimeChecked)
                    {
                        IsAllChecked = false;
                        IsPartTimeChecked = false;
                        List = new ObservableCollection<User>(DataProvider.Ins.DB.Users.Where(x => x.role_id == 2));
                        StaffSumarize = "Tổng số nhân viên: " + List.Count().ToString();

                    }
                }
            }
        }

        private bool _isPartTimeChecked;
        public bool IsPartTimeChecked
        {
            get { return _isPartTimeChecked; }
            set
            {
                if (_isPartTimeChecked != value)
                {
                    _isPartTimeChecked = value;
                    OnPropertyChanged();
                    if (_isPartTimeChecked)
                    {
                        IsAllChecked = false;
                        IsFullTimeChecked = false;
                        List = new ObservableCollection<User>(DataProvider.Ins.DB.Users.Where(x => x.role_id == 3));
                        StaffSumarize = "Tổng số nhân viên: " + List.Count().ToString();
                    }
                }
            }
        }

        private string staffSumarize;
        public string StaffSumarize { get { return staffSumarize; } set { staffSumarize = value; OnPropertyChanged(); } }

        private ObservableCollection<User> _List;
        public ObservableCollection<User> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        public static class InforNV
        {
            public static int Id { get; set; }
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
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ThemNhanVienCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ResetCommand { get; set; }


        public NhanVienViewModel()
        {
            EditCommand = new RelayCommand<User>(CanEdit, Edit);
            DeleteCommand = new RelayCommand<User>(CanEdit, Delete);
            ResetCommand = new RelayCommand<User>(CanEdit, Reset);
            SearchCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SearchAccount(); });
            ThemNhanVienCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                InforNV.Id = 0;
                InforNhanVien infoNhanVien = new InforNhanVien()
                {
                    DataContext = new InfoNhanVienViewModel(),
                };
                infoNhanVien.ShowDialog();
                List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
            });

            IsAllChecked = true;
            List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
            StaffSumarize = "Tổng số nhân viên: " + List.Count().ToString();
        }

        private bool CanEdit(User user)
        {
            return user != null;
        }

        private void Edit(User user)
        {
            InforNV.Id = user.user_id;
            InforNhanVien infoNhanVien = new InforNhanVien()
            {
                DataContext = new InfoNhanVienViewModel(),
            };
            infoNhanVien.ShowDialog();
            List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
        }
        private void Reset(User user)
        {
            var acc = DataProvider.Ins.DB.Users.FirstOrDefault(p=>p.user_id == user.user_id);
            acc.password = "0000";
            acc.security_question = "0000";
            acc.security_answer = "0000";
            DataProvider.Ins.DB.SaveChanges();
            System.Windows.MessageBox.Show("Đã reset tài khoản");
        }

        private void Delete(User user)
        {
            if (user != null)
            {
                var salaryHistory = DataProvider.Ins.DB.Salary_history.Where(p=>p.user_id == user.user_id);
                DataProvider.Ins.DB.Salary_history.RemoveRange(salaryHistory);
                var employeeShift = DataProvider.Ins.DB.EmployeeShifts.Where(p=>p.assigneduser_id== user.user_id);
                DataProvider.Ins.DB.EmployeeShifts.RemoveRange(employeeShift);

                // Xóa người dùng
                DataProvider.Ins.DB.Users.Remove(user);
                DataProvider.Ins.DB.SaveChanges();

                System.Windows.MessageBox.Show("Đã xóa tài khoản");

                // Xóa khỏi danh sách hiển thị
                List.Remove(user);
            }
        }

        void SearchAccount()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                List = new ObservableCollection<User>(DataProvider.Ins.DB.Users);
            }
            else
            {
                IQueryable<User> query = DataProvider.Ins.DB.Users;
                query = query.Where(db => db.full_name.ToLower().Contains(SearchText.ToLower()) 
                                    || db.username.ToLower().Contains(SearchText.ToLower())
                                    || db.email.ToLower().Contains(SearchText.ToLower())
                                    || db.phone.ToLower().Contains(SearchText.ToLower())
                                    || db.user_address.ToLower().Contains(SearchText.ToLower())
                                    );
                List = new ObservableCollection<User>(query);
            }
        }
    }
}
