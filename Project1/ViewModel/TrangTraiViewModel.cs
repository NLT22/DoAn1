using MyWeather;
using Newtonsoft.Json;
using Project1.Model;
using Project1.UserControlProject1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Project1.ViewModel
{

    public class TrangTraiViewModel : BaseViewModel
    {
        private readonly string apiKey = "67ba5ab753b84f85b07141755230405"; // Replace this with your Weather API key
        private string cityName;
        public string CityName { get { return cityName; } set { cityName = value; OnPropertyChanged(); } }

        private string weatherCondition;
        public string WeatherCondition { get { return weatherCondition; } set { weatherCondition = value; OnPropertyChanged(); } }

        private string temperature;
        public string Temperature { get { return temperature; } set { temperature = value; OnPropertyChanged(); } }

        private string humid;
        public string Humid { get { return humid; } set { humid = value; OnPropertyChanged(); } }

        private string windy;
        public string Windy { get { return windy; } set { windy = value; OnPropertyChanged(); } }

        private BitmapImage imgSource;
        public BitmapImage ImgSource { get { return imgSource; } set { imgSource = value; OnPropertyChanged(); } }

        private DateTime _now;
        public DateTime Now
        {
            get { return _now; }
            set
            {
                _now = value;
                OnPropertyChanged();
            }
        }


        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                UpdateVietnameseDate();
                LoadTaskShiftList();
                CongViecSummarize = "Công việc còn lại: " + TaskShiftList.Count().ToString();
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

        public string QuyMoTB
        {
            get => _QuyMoTB;
            set
            {

                _QuyMoTB = value;
                OnPropertyChanged();

            }
        }
        private string _QuyMoTB;
        public string NhanVienTB
        {
            get => _NhanVienTB;
            set
            {

                _NhanVienTB = value;
                OnPropertyChanged();

            }
        }
        private string _NhanVienTB;
        public string DoanhThuTB
        {
            get => _DoanhThuTB;
            set
            {
                _DoanhThuTB = value;
                OnPropertyChanged();
            }
        }
        private string _DoanhThuTB;

        private string _LonBenhTB;
        public string LonBenhTB
        {
            get => _LonBenhTB;
            set
            {
                _LonBenhTB = value;
                OnPropertyChanged();
            }
        }

        private string _LonMangThaiTB;
        public string LonMangThaiTB
        {
            get => _LonMangThaiTB;
            set
            {
                _LonMangThaiTB = value;
                OnPropertyChanged();
            }
        }

        private string _LonChoPhoiTB;
        public string LonChoPhoiTB
        {
            get => _LonChoPhoiTB;
            set
            {
                _LonChoPhoiTB = value;
                OnPropertyChanged();
            }
        }

        private string _LonCanTiemTB;
        public string LonCanTiemTB
        {
            get => _LonCanTiemTB;
            set
            {
                _LonCanTiemTB = value;
                OnPropertyChanged();
            }
        }

        private string _DuKienDeTB;
        public string DuKienDeTB
        {
            get => _DuKienDeTB;
            set
            {
                _DuKienDeTB = value;
                OnPropertyChanged();
            }
        }

        private string _QuaNgayDeTB;
        public string QuaNgayDeTB
        {
            get => _QuaNgayDeTB;
            set
            {
                _QuaNgayDeTB = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TaskShift> _TaskShiftList;
        public ObservableCollection<TaskShift> TaskShiftList
        {
            get => _TaskShiftList; set { _TaskShiftList = value; OnPropertyChanged(); }
        }
        public ICommand LoadWeather { get; set; }
        public ICommand CompletedCommand { get; set; }
        public ICommand DeleteTaskShiftCommand { get; set; }

        public TrangTraiViewModel()
        {
            WeatherConstructor();
            GetWeather();
            CaculateDoanhThu();
            LoadWeather = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                GetWeather();
            });
            SelectedDate = DateTime.Now;
            CompletedCommand = new RelayCommand<TaskShift>(CanCompleted, CompleteFunc);
            DeleteTaskShiftCommand = new RelayCommand<TaskShift>(CanCompleted, DeleteTaskShiftFunc);
            QuyMoTB = DataProvider.Ins.DB.Pigs.Where(p=>p.pen_id!=1).Count().ToString()+" con";
            NhanVienTB = DataProvider.Ins.DB.Users.Count().ToString()+ " người";

            LonBenhTB = DataProvider.Ins.DB.Pigs.Where(p=>p.health_status=="Bệnh").Count().ToString();
            LonMangThaiTB = DataProvider.Ins.DB.Events.Where(p => p.event_type == 3 && p.event_done==false).Count().ToString();
            LonChoPhoiTB = DataProvider.Ins.DB.Events.Where(p => p.event_type == 1 && p.event_done == false).Count().ToString();
            LonCanTiemTB = DataProvider.Ins.DB.Vacine_history.Where(p=>p.vacine_next_date==DateTime.Now).Count().ToString();
            DuKienDeTB = DataProvider.Ins.DB.Events.Where(p=>p.event_type == 3&&p.event_next_date== DateTime.Now).Count().ToString();
            QuaNgayDeTB = DataProvider.Ins.DB.Events.Where(p => p.event_type == 3 && p.event_next_date <= DateTime.Now).Count().ToString();
        }
        private string FormatToK(decimal? amount)
        {
            if (amount >= 1000)
            {
                return $"{amount / 1000:#,##0}k VND";
            }
            else
            {
                return $"{amount:#,##0} VND";
            }
        }
        public void CaculateDoanhThu()
        {
            var time = DateTime.Now; 
            var currentMonth = new DateTime(time.Year, time.Month, 1);
            var record = DataProvider.Ins.DB.MonthlyFinancialRecords.FirstOrDefault(p=>p.record_month==currentMonth);
            DoanhThuTB = FormatToK(record.pig_sell - record.food_cost - record.other_cost - record.employee_cost - record.medicine_cost);
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
        public void WeatherConstructor()
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                Now = DateTime.Now;
            };
            timer.Start();
            CityName = "Hanoi";
            Temperature = "";
            WeatherCondition = "";
            Humid = "";
            Windy = "";
        }
        private async void GetWeather()
        {
            string name = CityName.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a city name!");
                return;
            }

            string apiUrl = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={name}";

            try
            {
                HttpWebRequest request = WebRequest.CreateHttp(apiUrl);
                request.Method = "GET";

                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string jsonResponse = reader.ReadToEnd();
                            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);
                            DisplayWeatherData(weatherData);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                MessageBox.Show("An error occurred while fetching weather data: " + ex.Message);
            }
        }
        private void DisplayWeatherData(WeatherData weatherData)
        {
            CityName = weatherData.Location.Name;
            Temperature = weatherData.Current.TempC + "°C";
            WeatherCondition = weatherData.Current.Condition.Text;
            Humid = weatherData.Current.Humidity + "%";
            Windy = weatherData.Current.WindKph + " km/h";

            BitmapImage weatherIcon = new BitmapImage();
            weatherIcon.BeginInit();
            weatherIcon.UriSource = new Uri("http:" + weatherData.Current.Condition.Icon);
            weatherIcon.EndInit();
            ImgSource = weatherIcon;

        }
    }
}
