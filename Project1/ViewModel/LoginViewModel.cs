using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project1.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        // Lưu dữ liệu người dùng cho các ViewModel khác
        public static class SharedData
        {
            public static int Id { get; set; }
            public static string TenDangNhap { get; set; }
            public static int? Quyen { get; set; }
            public static string HoTen { get; set; }
            public static string CaiHoiBaoMat { get; set; }
            public static string CauTraLoi { get; set; }
            public static string MatKhau { get; set; }
            public static bool IsLogin { get; set; }
        }
        private string _TenDangNhap_TB;
        public string TenDangNhap_TB { get => _TenDangNhap_TB; set { _TenDangNhap_TB = value; OnPropertyChanged(); } }
        // Khởi tạo các lệnh đăng nhập, lấy dữ liệu mật khẩu và mở màn hình startup
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }
        public ICommand OpenFPWindowCommand { get; set; }

        public LoginViewModel()
        {
            SharedData.IsLogin = false;
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { SharedData.MatKhau = p.Password; });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            OpenFPWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { OpenFPWindow(p); });
        }
        void OpenFPWindow(Window p)
        {
            var UserAccount = DataProvider.Ins.DB.Users.FirstOrDefault(x => x.username == TenDangNhap_TB);
            if (UserAccount != null)
            {
                SharedData.HoTen = UserAccount.full_name;
                SharedData.TenDangNhap = UserAccount.username;
                SharedData.Quyen = UserAccount.role_id;
                SharedData.Id = UserAccount.user_id;
                SharedData.CaiHoiBaoMat = UserAccount.security_question;
                SharedData.CauTraLoi = UserAccount.security_answer;
                SharedData.MatKhau = UserAccount.password;

                ForgotPasswordWindow FPWindow = new ForgotPasswordWindow
                {
                    DataContext = new ForgotViewModel()
                };
                FPWindow.ShowDialog();

            }
            else
            {
                MessageBox.Show("Tên user không tồn tại");
            }
        }
        void Login(Window p)
        {
            if (p == null) return;
            var UserAccount = DataProvider.Ins.DB.Users.FirstOrDefault(x => x.username == TenDangNhap_TB && x.password == SharedData.MatKhau);
            if (UserAccount != null)
            {
                SharedData.HoTen = UserAccount.full_name;
                SharedData.IsLogin = true;
                SharedData.Id = UserAccount.user_id;
                SharedData.TenDangNhap = UserAccount.username;
                SharedData.Quyen = UserAccount.role_id;
                p.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }

    }

}
