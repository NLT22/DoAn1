using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using static Project1.ViewModel.LoginViewModel;
using static Project1.ViewModel.NhanVienViewModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Project1.ViewModel.NhanViewManagement
{
    public class InfoNhanVienViewModel : BaseViewModel
    {
        public string Username_TB { get => _Username_TB; set { _Username_TB = value; OnPropertyChanged(); } }
        private string _Username_TB;

        public string Fullname_TB { get => _Fullname_TB; set { _Fullname_TB = value; OnPropertyChanged(); } }
        private string _Fullname_TB;

        public string Password_TB { get => _Password_TB; set { _Password_TB = value; OnPropertyChanged(); } }
        private string _Password_TB;

        public string Address_TB { get => _Address_TB; set { _Address_TB = value; OnPropertyChanged(); } }
        private string _Address_TB;


        public string Security_TB { get => _Security_TB; set { _Security_TB = value; OnPropertyChanged(); } }
        private string _Security_TB;

        public string Answer_TB { get => _Answer_TB; set { _Answer_TB = value; OnPropertyChanged(); } }
        private string _Answer_TB;

        public string Phone_TB { get => _Phone_TB; set { _Phone_TB = value; OnPropertyChanged(); } }
        private string _Phone_TB;
        public string Email_TB { get => _Email_TB; set { _Email_TB = value; OnPropertyChanged(); } }
        private string _Email_TB;

        public DateTime? Birth_TB { get => _Birth_TB; set { _Birth_TB = value; OnPropertyChanged(); } }
        private DateTime? _Birth_TB;

        private Visibility _AccountVisibility;
        public Visibility AccountVisibility
        {
            get => _AccountVisibility;
            set
            {
                _AccountVisibility = value;
                OnPropertyChanged();
            }
        }
        public Role RoleSelectedItem { get => _roleSelectedItem; set { _roleSelectedItem = value; OnPropertyChanged(); } }
        private Role _roleSelectedItem;

        public ObservableCollection<Role> RoleList { get => _RoleList; set { _RoleList = value; OnPropertyChanged(); } }
        private ObservableCollection<Role> _RoleList;

        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public InfoNhanVienViewModel()
        {
            RoleList = new ObservableCollection<Role>(DataProvider.Ins.DB.Roles);
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
            var UserAccount = DataProvider.Ins.DB.Users.FirstOrDefault(x => x.user_id == InforNV.Id);
            if (UserAccount.user_id == SharedData.Id)
            {
                Fullname_TB = UserAccount.full_name;
                Username_TB = UserAccount.username;
                Security_TB = UserAccount.security_question;
                Answer_TB = UserAccount.security_answer;
                Password_TB = UserAccount.password;
                Birth_TB = UserAccount.birth;
                Address_TB = UserAccount.user_address;
                Phone_TB = UserAccount.phone;
                Email_TB = UserAccount.email;
                RoleSelectedItem = UserAccount.Role;
                AccountVisibility = Visibility.Visible;
            }
            else
            {
                Fullname_TB = UserAccount.full_name;
                Birth_TB = UserAccount.birth;
                Address_TB = UserAccount.user_address;
                Phone_TB = UserAccount.phone;
                Email_TB = UserAccount.email;
                RoleSelectedItem = UserAccount.Role;
                AccountVisibility = Visibility.Collapsed;
            }
        }
        void SaveNV()
        {
            var user = DataProvider.Ins.DB.Users.FirstOrDefault(x => x.username == Username_TB);
            if (user != null)
            {
                if (user.user_id == SharedData.Id)
                {
                    user.full_name = Fullname_TB;
                    user.username = Username_TB;
                    user.user_address = Address_TB;
                    user.role_id = RoleSelectedItem.role_id;
                    user.security_answer = Answer_TB;
                    user.security_question = Security_TB;
                    user.birth = Birth_TB;
                    user.password = Password_TB;
                    user.phone = Phone_TB;
                    user.email = Email_TB;
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin người dùng thành công");
                    return;
                }
                user.full_name = Fullname_TB;
                user.user_address = Address_TB;
                user.role_id = RoleSelectedItem.role_id;
                user.birth = Birth_TB;
                user.phone = Phone_TB;
                user.email = Email_TB;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Cập nhật thông tin người dùng thành công");
                return;
            }
            var NewUser = new User
            {
                full_name = Fullname_TB,
                username = Username_TB,
                user_address = Address_TB,
                role_id = RoleSelectedItem.role_id,
                security_answer = Answer_TB,
                security_question = Security_TB,
                birth = Birth_TB,
                password = Password_TB,
                phone = Phone_TB,
                email = Email_TB,
            };
            DataProvider.Ins.DB.Users.Add(NewUser);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm tài khoản thành công");
        }
    }
}
