using LiveCharts.Wpf;
using LiveCharts;
using Project1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Project1.ViewModel.QuanLyLonNuoiViewModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;

namespace Project1.ViewModel.PigManagementViewModel
{
    public class NewPigViewModel : BaseViewModel
    {
        private Visibility _generalVisibility;
        public Visibility GeneralVisibility
        {
            get => _generalVisibility;
            set
            {
                _generalVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _weightLengthVisibility;
        public Visibility WeightLengthVisibility
        {
            get => _weightLengthVisibility;
            set
            {
                _weightLengthVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _vaccineVisibility;
        public Visibility VaccineVisibility
        {
            get => _vaccineVisibility;
            set
            {
                _vaccineVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _DiseaseVisibility;
        public Visibility DiseaseVisibility
        {
            get => _DiseaseVisibility;
            set
            {
                _DiseaseVisibility = value;
                OnPropertyChanged();
            }
        }
        private bool _isGeneralChecked;
        public bool IsGeneralChecked
        {
            get { return _isGeneralChecked; }
            set
            {
                if (_isGeneralChecked != value)
                {
                    _isGeneralChecked = value;
                    OnPropertyChanged();
                    if (_isGeneralChecked)
                    {
                        IsWeightLengthChecked = false;
                        IsVaccineChecked = false;
                        IsDiseaseChecked = false;

                        UpdateVisibility();

                    }
                }
            }
        }

        private bool _isWeightLengthChecked;
        public bool IsWeightLengthChecked
        {
            get { return _isWeightLengthChecked; }
            set
            {
                if (_isWeightLengthChecked != value)
                {
                    _isWeightLengthChecked = value;
                    OnPropertyChanged();
                    if (_isWeightLengthChecked)
                    {
                        IsGeneralChecked = false;
                        IsVaccineChecked = false;
                        IsDiseaseChecked = false;

                        LoadPigMeasurements();
                        UpdateVisibility();

                    }
                }
            }
        }


        private bool _isVaccineChecked;
        public bool IsVaccineChecked
        {
            get { return _isVaccineChecked; }
            set
            {
                if (_isVaccineChecked != value)
                {
                    _isVaccineChecked = value;
                    OnPropertyChanged();
                    if (_isVaccineChecked)
                    {
                        IsWeightLengthChecked = false;
                        IsGeneralChecked = false;
                        IsDiseaseChecked = false;

                        UpdateVisibility();
                    }
                }
            }
        }
        private bool _isDiseaseChecked;
        public bool IsDiseaseChecked
        {
            get { return _isDiseaseChecked; }
            set
            {
                if (_isDiseaseChecked != value)
                {
                    _isDiseaseChecked = value;
                    OnPropertyChanged();
                    if (_isDiseaseChecked)
                    {
                        IsWeightLengthChecked = false;
                        IsGeneralChecked = false;
                        IsVaccineChecked = false;

                        UpdateVisibility();
                    }
                }
            }
        }
        public Pig PigSelectedItem
        {
            get => _PigSelectedItem;
            set
            {
                _PigSelectedItem = value;
                if (value != null) InforPig.Id = _PigSelectedItem.pig_id;
                LoadInfor();
                OnPropertyChanged();
            }
        }
        private Pig _PigSelectedItem;

        public ObservableCollection<Pig> Pig_List { get => _Pig_List; set { _Pig_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Pig> _Pig_List;
        public PigPen PenSelectedItem { get => _PenSelectedItem; set { _PenSelectedItem = value; OnPropertyChanged(); } }
        private PigPen _PenSelectedItem;

        public ObservableCollection<PigPen> PigPen_List { get => _PigPen_List; set { _PigPen_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigPen> _PigPen_List;



        //Phần khai báo cho trang general
        public ObservableCollection<PigMeasurement> Measure_List { get => _Measure_List; set { _Measure_List = value; OnPropertyChanged(); } }
        private ObservableCollection<PigMeasurement> _Measure_List;
        public string PigType_TB { get => _PigType_TB; set { _PigType_TB = value; OnPropertyChanged(); } }
        private string _PigType_TB;

        public string Gender_TB { get => _Gender_TB; set { _Gender_TB = value; OnPropertyChanged(); } }
        private string _Gender_TB;

        public string Health_TB { get => _Health_TB; set { _Health_TB = value; OnPropertyChanged(); } }
        private string _Health_TB;
        public string Origin_TB { get => _Origin_TB; set { _Origin_TB = value; OnPropertyChanged(); } }
        private string _Origin_TB;
        public string PigNumber_TB { get => _PigNumber_TB; set { _PigNumber_TB = value; OnPropertyChanged(); } }
        private string _PigNumber_TB;

        public DateTime? Birth_TB { get => _Birth_TB; set { _Birth_TB = value; OnPropertyChanged(); } }
        private DateTime? _Birth_TB;
        public DateTime? Import_TB { get => _Import_TB; set { _Import_TB = value; OnPropertyChanged(); } }
        private DateTime? _Import_TB;
        public decimal Price_TB { get => _Price_TB; set { _Price_TB = value; OnPropertyChanged(); } }
        private decimal _Price_TB;


        //Phần khai báo cho trang meausure
        public string Weight_TB { get => _Weight_TB; set { _Weight_TB = value; OnPropertyChanged(); } }
        private string _Weight_TB;

        public string Length_TB { get => _Length_TB; set { _Length_TB = value; OnPropertyChanged(); } }
        private string _Length_TB;

        public DateTime? MeasureDate_TB { get => _MeasureDate_TB; set { _MeasureDate_TB = value; OnPropertyChanged(); } }
        private DateTime? _MeasureDate_TB;

        public SeriesCollection PigDataSeries { get; set; }
        public List<string> Dates { get; set; }

        //Phần khai báo cho trang Tiêm phòng
        public string VaccineName_TB { get => _VaccineName_TB; set { _VaccineName_TB = value; OnPropertyChanged(); } }
        private string _VaccineName_TB;

        public string VaccineCount_TB { get => _VaccineCount_TB; set { _VaccineCount_TB = value; OnPropertyChanged(); } }
        private string _VaccineCount_TB;

        public string WaitTime_TB { get => _WaitTime_TB; set { _WaitTime_TB = value; OnPropertyChanged(); } }
        private string _WaitTime_TB;
        public ObservableCollection<Vacine_history> Vaccine_List { get => _Vaccine_List; set { _Vaccine_List = value; OnPropertyChanged(); } }
        private ObservableCollection<Vacine_history> _Vaccine_List;

        private bool _AllVaccineChecked;
        public bool AllVaccineChecked
        {
            get => _AllVaccineChecked;
            set
            {
                _AllVaccineChecked = value;
                OnPropertyChanged();

                if (value)
                {
                    AllVaccineCheckedCommand.Execute(null);
                }
            }
        }

        private bool _NotDoneVacineChecked;
        public bool NotDoneVacineChecked
        {
            get => _NotDoneVacineChecked;
            set
            {
                _NotDoneVacineChecked = value;
                OnPropertyChanged();
                if (value)
                {
                    NotDoneVacineCheckedCommand.Execute(null);
                }
            }
        }
        private string _searchVaccineText;
        public string SearchVaccineText
        {
            get => _searchVaccineText;
            set
            {
                _searchVaccineText = value;
                OnPropertyChanged();
                SearchVaccineCommand.Execute(null);
            }
        }

        //Phần khai báo cho trang Dịch bệnh
        public string DiseaseName_TB { get => _DiseaseName_TB; set { _DiseaseName_TB = value; OnPropertyChanged(); } }
        private string _DiseaseName_TB;

        public DateTime? DiseaseDate_TB { get => _DiseaseDate_TB; set { _DiseaseDate_TB = value; OnPropertyChanged(); } }
        private DateTime? _DiseaseDate_TB;

        public string DiseaseDecription_TB { get => _DiseaseDecription_TB; set { _DiseaseDecription_TB = value; OnPropertyChanged(); } }
        private string _DiseaseDecription_TB;
        public ObservableCollection<SickPig> Disease_List { get => _Disease_List; set { _Disease_List = value; OnPropertyChanged(); } }
        private ObservableCollection<SickPig> _Disease_List;

        private bool _AllDiseaseChecked;
        public bool AllDiseaseChecked
        {
            get => _AllDiseaseChecked;
            set
            {
                _AllDiseaseChecked = value;
                OnPropertyChanged();

                if (value)
                {
                    AllDiseaseCheckedCommand.Execute(null);
                }
            }
        }

        private bool _NotDoneDiseaseChecked;
        public bool NotDoneDiseaseChecked
        {
            get => _NotDoneDiseaseChecked;
            set
            {
                _NotDoneDiseaseChecked = value;
                OnPropertyChanged();
                if (value)
                {
                    NotDoneDiseaseCheckedCommand.Execute(null);
                }
            }
        }
        private string _SearchDiseaseText;
        public string SearchDiseaseText
        {
            get => _SearchDiseaseText;
            set
            {
                _SearchDiseaseText = value;
                OnPropertyChanged();
                SearchDiseaseCommand.Execute(null);
            }
        }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand AllVaccineCheckedCommand { get; set; }
        public ICommand NotDoneVacineCheckedCommand { get; set; }
        public ICommand AddVacineCommand { get; set; }
        public ICommand NewVaccineCommand { get; set; }
        public ICommand NewDiseaseCommand { get; set; }
        public ICommand SearchVaccineCommand { get; set; }
        public ICommand SearchDiseaseCommand { get; set; }
        public ICommand AllDiseaseCheckedCommand { get; set; }
        public ICommand NotDoneDiseaseCheckedCommand { get; set; }
        public ICommand DiseaseDoneCommand { get; set; }
        public ICommand ReDiseaseCommand { get; set; }

        public NewPigViewModel()
        {
            Pig_List = new ObservableCollection<Pig>(DataProvider.Ins.DB.Pigs);
            PigSelectedItem = DataProvider.Ins.DB.Pigs.FirstOrDefault(p => p.pig_id == InforPig.Id);

            GeneralVisibility = Visibility.Visible;
            WeightLengthVisibility = Visibility.Collapsed;
            VaccineVisibility = Visibility.Collapsed;
            DiseaseVisibility = Visibility.Collapsed;
            IsGeneralChecked = true;
            //Dịch bệnh
            AllDiseaseCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoadAllDiseaseChecked(); });
            NotDoneDiseaseCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoadNotDoneDiseaseChecked(); });
            SearchDiseaseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SearchDisease(); });
            NewDiseaseCommand = new RelayCommand<object>(
                (p) => { return DiseaseName_TB != null && DiseaseDecription_TB != null && DiseaseDate_TB != null; },
                (p) => { NewDisease(); });
            DiseaseDoneCommand = new RelayCommand<SickPig>(CanDiseaseDone, DiseaseDone);
            ReDiseaseCommand = new RelayCommand<SickPig>(CanReDisease, ReDisease);

            DiseaseDate_TB = DateTime.Now;

            //tiêm phòng
            AllVaccineCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoadAllVaccineChecked(); });
            NotDoneVacineCheckedCommand = new RelayCommand<object>((p) => { return true; }, (p) => { LoadNotDoneVaccineChecked(); });

            AddVacineCommand = new RelayCommand<Vacine_history>(CanAddVacince, AddVacine);
            LoadInfor();
            AllVaccineChecked = true;
            AllDiseaseChecked = true;

            NewVaccineCommand = new RelayCommand<object>(
                (p) => { return VaccineName_TB != null && VaccineCount_TB != null && WaitTime_TB != null; },
                (p) => { NewVaccine(); });
            SaveCommand = new RelayCommand<Window>((p) => { return SaveCondition(); }, (p) => { SaveNV(p); });
            SearchVaccineCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SearchVaccine(); });

            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { CloseWindow(p); });
        }
        //Dịch bệnh
        private void DiseaseDone(SickPig sick)
        {
            sick.disease_done = true;
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Lợn đã khỏi bệnh");
            if (AllDiseaseChecked) LoadAllDiseaseChecked();
            if (NotDoneDiseaseChecked) LoadNotDoneDiseaseChecked();
        }
        private void ReDisease(SickPig sick)
        {
            var newSick = new SickPig
            {
                description = sick.description,
                detection_date = DateTime.Now,
                disease_done = false,
                disease_name = sick.disease_name,
                pig_id = sick.pig_id
            };

            DataProvider.Ins.DB.SickPigs.Add(newSick);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Lợn đã tái phát bệnh");
            if (AllDiseaseChecked) LoadAllDiseaseChecked();
            if (NotDoneDiseaseChecked) LoadNotDoneDiseaseChecked();
        }
        private bool CanReDisease(SickPig sick)
        {
            return sick.disease_done == true;
        }
        private bool CanDiseaseDone(SickPig sick)
        {
            return sick.disease_done == false;
        }
        public void LoadAllDiseaseChecked()
        {
            Disease_List = new ObservableCollection<SickPig>(DataProvider.Ins.DB.SickPigs.Where(p => p.pig_id == InforPig.Id));

        }
        public void LoadNotDoneDiseaseChecked()
        {
            Disease_List = new ObservableCollection<SickPig>(DataProvider.Ins.DB.SickPigs.Where(p => p.pig_id == InforPig.Id && p.disease_done == false));
            var pig = DataProvider.Ins.DB.Pigs.SingleOrDefault(x => x.pig_id == InforPig.Id);
            if (Disease_List.Count > 0)
            {
                pig.health_status = "Bệnh";
            }
            else
            {
                pig.health_status = "Khỏe mạnh";
            }
            DataProvider.Ins.DB.SaveChanges();
        }
        void SearchDisease()
        {
            if (string.IsNullOrEmpty(SearchDiseaseText))
            {
                if (AllDiseaseChecked) LoadAllDiseaseChecked();
                if (NotDoneDiseaseChecked) LoadNotDoneDiseaseChecked();
            }
            else
            {
                IQueryable<SickPig> query = DataProvider.Ins.DB.SickPigs;
                query = query.Where(db =>
                    (db.disease_name.ToLower().Contains(SearchDiseaseText.ToLower())
                    || db.detection_date.ToString().ToLower().Contains(SearchDiseaseText.ToLower())
                    || db.description.ToLower().Contains(SearchDiseaseText.ToLower()))
                    && db.pig_id == InforPig.Id
                );
                Disease_List = new ObservableCollection<SickPig>(query);
            }
        }
        public void NewDisease()
        {
            var newSick = new SickPig
            {
                description = DiseaseDecription_TB,
                detection_date = DiseaseDate_TB,
                disease_done = false,
                disease_name = DiseaseName_TB,
                pig_id = InforPig.Id,
            };
            DataProvider.Ins.DB.SickPigs.Add(newSick);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm bệnh thành công");
            if (AllDiseaseChecked) LoadAllDiseaseChecked();
            if (NotDoneDiseaseChecked) LoadNotDoneDiseaseChecked();
        }

        //tiêm phòng
        public bool SaveCondition()
        {
            if (IsGeneralChecked)
            {
                return Origin_TB != null && PigType_TB != null && Gender_TB != null && Health_TB != null && Birth_TB != null && Import_TB != null && PenSelectedItem != null;
            }
            if (IsWeightLengthChecked)
            {
                return MeasureDate_TB != null && Weight_TB != null && Length_TB != null;
            }
            if (IsDiseaseChecked)
            {
                return DiseaseName_TB != null && DiseaseDecription_TB != null && DiseaseDate_TB != null;
            }
            return false;
        }
        void SearchVaccine()
        {
            if (string.IsNullOrEmpty(SearchVaccineText))
            {
                if (AllVaccineChecked) LoadAllVaccineChecked();
                if (NotDoneVacineChecked) LoadNotDoneVaccineChecked();
            }
            else
            {
                IQueryable<Vacine_history> query = DataProvider.Ins.DB.Vacine_history;
                query = query.Where(db =>
                (db.vacine_name.ToLower().Contains(SearchVaccineText.ToLower())
                || db.vacine_date.ToString().Contains(SearchVaccineText.ToLower())
                || db.vacine_next_date.ToString().Contains(SearchVaccineText.ToLower()))
                && db.pig_id == InforPig.Id
                );
                Vaccine_List = new ObservableCollection<Vacine_history>(query);
            }
        }
        public void NewVaccine()
        {
            var newHistory = new Vacine_history
            {
                vacine_name = VaccineName_TB,
                vacine_date = DateTime.Now,
                vacine_count = 1,
                vacine_duration = int.Parse(WaitTime_TB),
                require_count = int.Parse(VaccineCount_TB),
                pig_id = InforPig.Id,
                vacine_done = int.Parse(WaitTime_TB) == 1
            };
            DataProvider.Ins.DB.Vacine_history.Add(newHistory);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm mũi vaccine thành công");
            if (AllVaccineChecked) LoadAllVaccineChecked();
            if (NotDoneVacineChecked) LoadNotDoneVaccineChecked();
        }
        private bool CanAddVacince(Vacine_history history)
        {
            return history.vacine_done == false;
        }
        private void AddVacine(Vacine_history history)
        {
            var newHistory = new Vacine_history
            {
                vacine_name = history.vacine_name,
                vacine_date = DateTime.Now,
                vacine_count = history.vacine_count + 1,
                vacine_duration = history.vacine_duration,
                require_count = history.require_count,
                pig_id = history.pig_id,
                vacine_done = history.vacine_count + 1 >= history.require_count
            };

            history.vacine_done = true;
            if (newHistory.vacine_count == newHistory.require_count) newHistory.vacine_done = true;

            DataProvider.Ins.DB.Vacine_history.Add(newHistory);
            DataProvider.Ins.DB.SaveChanges();
            MessageBox.Show("Thêm mũi vaccine thành công");
            if (AllVaccineChecked) LoadAllVaccineChecked();
            if (NotDoneVacineChecked) LoadNotDoneVaccineChecked();
        }
        public void LoadInfor()
        {
            LoadGeneral();
            LoadPigMeasurements();
            LoadAllVaccineChecked();
            LoadAllDiseaseChecked();
        }
        public void LoadAllVaccineChecked()
        {
            Vaccine_List = new ObservableCollection<Vacine_history>(DataProvider.Ins.DB.Vacine_history.Where(p => p.pig_id == InforPig.Id));
        }
        public void LoadNotDoneVaccineChecked()
        {
            Vaccine_List = new ObservableCollection<Vacine_history>(DataProvider.Ins.DB.Vacine_history.Where(p => p.pig_id == InforPig.Id && p.vacine_done == false));
        }
        public void LoadGeneral()
        {
            Pig pig = DataProvider.Ins.DB.Pigs.FirstOrDefault(p => p.pig_id == InforPig.Id);
            PigType_TB = pig.pig_type;
            Gender_TB = pig.gender;
            Health_TB = pig.health_status;
            Birth_TB = pig.date_of_birth;
            Origin_TB = pig.origin;
            Import_TB = pig.date_of_arrival;
            PigPen_List = new ObservableCollection<PigPen>(DataProvider.Ins.DB.PigPens);
            PenSelectedItem = pig.PigPen;
        }
        public void LoadPigMeasurements()
        {
            // Get pig measurements from the database
            var pigMeasurements = DataProvider.Ins.DB.PigMeasurements.Where(p => p.pig_id == InforPig.Id);

            Measure_List = new ObservableCollection<PigMeasurement>(DataProvider.Ins.DB.PigMeasurements.Where(p => p.pig_id == InforPig.Id));
            MeasureDate_TB = DateTime.Now;

            // Initialize lists for chart data
            var newPigDataSeries = new SeriesCollection();
            var newDates = new List<string>();

            // Initialize series for weight and length
            var weightSeries = new LineSeries
            {
                Title = "Cân nặng",
                Values = new ChartValues<double>(),
                Stroke = new SolidColorBrush(Color.FromRgb(0x9D, 0xDE, 0x8B)), // Màu #9DDE8B
                Fill = new SolidColorBrush(Color.FromRgb(0x9D, 0xDE, 0x8B)) { Opacity = 0.2 } // Giảm opacity của Fill
            };

            var lengthSeries = new LineSeries
            {
                Title = "Chiều dài",
                Values = new ChartValues<double>(),
                Stroke = new SolidColorBrush(Color.FromRgb(0x40, 0xA5, 0x78)), // Màu #40A578
                Fill = new SolidColorBrush(Color.FromRgb(0x40, 0xA5, 0x78)) { Opacity = 0.2 } // Giảm opacity của Fill
            };

            // Loop through each pig measurement and add data to lists
            foreach (var measurement in pigMeasurements)
            {
                // Add measurement date to dates list
                newDates.Add(measurement.date_of_measurement?.ToString("MM/dd/yyyy"));

                // Add weight and length to corresponding series
                weightSeries.Values.Add((double)measurement.weight);
                lengthSeries.Values.Add((double)measurement.length);
            }

            // Add weight and length series to the collection
            newPigDataSeries.Add(weightSeries);
            newPigDataSeries.Add(lengthSeries);

            // Update the property values and notify UI of the change
            PigDataSeries = newPigDataSeries;
            Dates = newDates;

            OnPropertyChanged(nameof(PigDataSeries));
            OnPropertyChanged(nameof(Dates));
        }
        private void UpdateVisibility()
        {
            GeneralVisibility = IsGeneralChecked ? Visibility.Visible : Visibility.Collapsed;
            WeightLengthVisibility = IsWeightLengthChecked ? Visibility.Visible : Visibility.Collapsed;
            VaccineVisibility = IsVaccineChecked ? Visibility.Visible : Visibility.Collapsed;
            DiseaseVisibility = IsDiseaseChecked ? Visibility.Visible : Visibility.Collapsed;
        }
        void CloseWindow(Window x)
        {
            var w = Window.GetWindow(x);
            if (w != null)
            {
                w.Close();
            }
        }
        void SaveNV(Window p)
        {
            var pig = DataProvider.Ins.DB.Pigs.SingleOrDefault(x => x.pig_id == InforPig.Id);
            if (IsGeneralChecked)
            {
                if (pig != null)
                {
                    // Cập nhật thông tin lợn hiện tại
                    pig.pig_type = PigType_TB;
                    pig.gender = Gender_TB;
                    pig.health_status = Health_TB;
                    pig.date_of_birth = Birth_TB;
                    pig.origin = Origin_TB;
                    pig.date_of_arrival = Import_TB;
                    pig.pen_id = PenSelectedItem.pen_id;
                    DataProvider.Ins.DB.SaveChanges();

                    // Tạo các bản ghi lợn mới
                    int numberOfPigsToCreate = int.Parse(PigNumber_TB) - 1;
                    for (int i = 0; i < numberOfPigsToCreate; i++)
                    {
                        var newPig = new Pig
                        {
                            pig_type = pig.pig_type,
                            gender = pig.gender,
                            health_status = pig.health_status,
                            date_of_birth = pig.date_of_birth,
                            origin = pig.origin,
                            date_of_arrival = pig.date_of_arrival,
                            pen_id = pig.pen_id
                        };
                        DataProvider.Ins.DB.Pigs.Add(newPig);
                        // Sao chép các bản ghi liên quan
                        CopyRelatedData(pig.pig_id, newPig.pig_id);
                    }
                    var cost = new PenExpense
                    {
                        expense_date = DateTime.Now,
                        pen_id = pig.pen_id,
                        expense = Price_TB,
                        pig_sell = false,
                        expense_description = "Nhập lợn " + PigNumber_TB + " vào chuồng '" + pig.PigPen.pen_name.ToString() + "' vào ngày " + DateTime.Now.ToShortDateString(),
                    };
                    DataProvider.Ins.DB.PenExpenses.Add(cost);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Cập nhật thông tin lợn thành công");
                    p.Close();
                }
            }
            if (IsWeightLengthChecked)
            {
                if (pig != null)
                {
                    var newMeasure = new PigMeasurement
                    {
                        date_of_measurement = MeasureDate_TB,
                        weight = (decimal?)double.Parse(Weight_TB),
                        length = (decimal?)double.Parse(Length_TB),
                        pig_id = InforPig.Id
                    };
                    DataProvider.Ins.DB.PigMeasurements.Add(newMeasure);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm thông số đo mới thành công");
                    LoadPigMeasurements();
                }
            }
            if (IsDiseaseChecked)
            {
                if (pig != null)
                {
                    var newSichPig = new SickPig
                    {
                        disease_name = DiseaseName_TB,
                        detection_date = DiseaseDate_TB,
                        description = DiseaseDecription_TB,
                        disease_done = false,
                        pig_id = InforPig.Id
                    };
                    DataProvider.Ins.DB.SickPigs.Add(newSichPig);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm bệnh mới thành công");
                }
            }
        }
        void CopyRelatedData(int oldPigId, int newPigId)
        {
            CopyVacineHistory(oldPigId, newPigId);
            CopyPigMeasurements(oldPigId, newPigId);
            CopySickPigs(oldPigId, newPigId);
        }
        void CopyVacineHistory(int oldPigId, int newPigId)
        {
            var vacineHistories = DataProvider.Ins.DB.Vacine_history.Where(v => v.pig_id == oldPigId).ToList();
            foreach (var vacineHistory in vacineHistories)
            {
                var newVacineHistory = new Vacine_history
                {
                    vacine_name = vacineHistory.vacine_name,
                    vacine_date = vacineHistory.vacine_date,
                    require_count = vacineHistory.require_count,
                    vacine_count = vacineHistory.vacine_count,
                    vacine_done = vacineHistory.vacine_done,
                    vacine_duration = vacineHistory.vacine_duration,
                    pig_id = newPigId
                };
                DataProvider.Ins.DB.Vacine_history.Add(newVacineHistory);
            }
            DataProvider.Ins.DB.SaveChanges();
        }
        void CopyPigMeasurements(int oldPigId, int newPigId)
        {
            var pigMeasurements = DataProvider.Ins.DB.PigMeasurements.Where(m => m.pig_id == oldPigId).ToList();
            foreach (var pigMeasurement in pigMeasurements)
            {
                var newPigMeasurement = new PigMeasurement
                {
                    weight = pigMeasurement.weight,
                    length = pigMeasurement.length,
                    date_of_measurement = pigMeasurement.date_of_measurement,
                    pig_id = newPigId
                };
                DataProvider.Ins.DB.PigMeasurements.Add(newPigMeasurement);
            }
            DataProvider.Ins.DB.SaveChanges();
        }
        void CopySickPigs(int oldPigId, int newPigId)
        {
            var sickPigs = DataProvider.Ins.DB.SickPigs.Where(s => s.pig_id == oldPigId).ToList();
            foreach (var sickPig in sickPigs)
            {
                var newSickPig = new SickPig
                {
                    disease_name = sickPig.disease_name,
                    disease_done = sickPig.disease_done,
                    detection_date = sickPig.detection_date,
                    description = sickPig.description,
                    pig_id = newPigId
                };
                DataProvider.Ins.DB.SickPigs.Add(newSickPig);
            }
            DataProvider.Ins.DB.SaveChanges();
        }
    }
}

