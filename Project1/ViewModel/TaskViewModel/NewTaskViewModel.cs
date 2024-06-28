using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.PigManagementViewModel.EventDecriptionViewModel;
using Task = Project1.Model.Task;

namespace Project1.ViewModel.TaskViewModel
{
    public class NewTaskViewModel : BaseViewModel
    {
        public string Description_TB { get => _Description_TB; set { _Description_TB = value; OnPropertyChanged(); } }
        private string _Description_TB;
        public string TaskName_TB { get => _TaskName_TB; set { _TaskName_TB = value; OnPropertyChanged(); } }
        private string _TaskName_TB;
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SaveTaskCommand { get; set; }
        public NewTaskViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { CloseWindow(p); });

            SaveTaskCommand = new RelayCommand<Window>(
                (p) => { return Description_TB != null && TaskName_TB != null; },
                (p) =>
                {
                    var newTask = new Task
                    {
                        task_name = TaskName_TB,
                        description = Description_TB
                    };
                    DataProvider.Ins.DB.Tasks.Add(newTask);
                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Thêm công việc mới thành công");
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
    }
}
