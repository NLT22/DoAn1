using Project1.FeatureWindow;
using Project1.Model;
using Project1.UserControlProject1;
using Project1.ViewModel.NhanViewManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.LoginViewModel;
using static Project1.ViewModel.NhanVienViewModel;

namespace Project1.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        //UserControl cho trang tru
        private object _currentControl = new TrangTraiUC();
        public object CurrentControl { get { return _currentControl; } set { if (_currentControl != value) { _currentControl = value; OnPropertyChanged(); } } }


        //Dang nhap
        private string _hoTen;
        private string _tenDangNhap;
        private string _quyen;
        public string TenDangNhap { get => _tenDangNhap; set { _tenDangNhap = value; OnPropertyChanged(); } }
        public string HoTen { get => _hoTen; set { _hoTen = value; OnPropertyChanged(); } }
        public string Quyen { get => _quyen; set { _quyen = value; OnPropertyChanged(); } }

        public bool Isloaded = false;

        //Cac lenh command
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LoadExitAccCommand { get; set; }
        public ICommand LoadTraiTrangCommand { get; set; }
        public ICommand LoadLonNuoiCommand { get; set; }
        public ICommand LoadNhanVienCommand { get; set; }
        public ICommand LoadKhoCommand { get; set; }
        public ICommand LoadCongViecCommand { get; set; }
        public ICommand LoadTaiChinhCommand { get; set; }
        public ICommand LoadThietLapCommand { get; set; }
        public ICommand LoadInforCommand { get; set; }
        public MainWindowViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
                if (SharedData.IsLogin)
                {
                    HoTen = SharedData.HoTen;
                    switch (SharedData.Quyen)
                    {
                        case (1): Quyen = "Quản trị viên"; break;
                        case (2): Quyen = "Nhân viên fulltime"; break;
                        case (3): Quyen = "Nhân viên partime"; break;
                    }
                    p.Show();
                }
            });

            LoadExitAccCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SharedData.IsLogin = false;
                p.Hide();
                p = new MainWindow
                {
                    DataContext = new MainWindowViewModel()
                };
                p.Show();
            });

            LoadTraiTrangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentControl = new TrangTraiUC() { DataContext = new TrangTraiViewModel() };
            });

            LoadLonNuoiCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentControl = new QuanLyLonNuoiUC() { DataContext = new QuanLyLonNuoiViewModel() };
            });

            LoadKhoCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CurrentControl = new KhoUC() { DataContext = new QLKhoViewModel() };
            });

            LoadInforCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                InforNV.Id = SharedData.Id;
                InforNhanVien infoNhanVien = new InforNhanVien()
                {
                    DataContext = new InfoNhanVienViewModel(),
                };
                infoNhanVien.ShowDialog();
            });


            LoadNhanVienCommand = new RelayCommand<object>((p) => {  return IsAuthorized(); }, (p) =>
            {
                CurrentControl = new NhanVienUC() { DataContext = new NhanVienViewModel() };
            });

            LoadCongViecCommand = new RelayCommand<object>((p) => { return IsAuthorized(); }, (p) =>
            {
                CurrentControl = new CongViecUC() { DataContext = new CongViecViewModel() };
            });

            LoadThietLapCommand = new RelayCommand<object>((p) => { return IsAuthorized(); }, (p) =>
            {
                CurrentControl = new ThietLapUC() { DataContext = new ThietLapViewModel() };
            });

            LoadTaiChinhCommand = new RelayCommand<object>((p) => { return IsAuthorized(); }, (p) =>
            {
                CurrentControl = new TaiChinhUC() { DataContext = new TaiChinhViewModel() };
            });


        }
        public bool IsAuthorized()
        {
            return SharedData.Quyen == 1;
        }
        public void unAuthorized()
        {
            MessageBox.Show("Bạn không có quyền truy cập mục này");
        }
    }
}
