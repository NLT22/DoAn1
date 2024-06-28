using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project1.ViewModel.PigManagementViewModel
{
    public class EventDecriptionViewModel : BaseViewModel
    {
        public static class Decription
        {
            public static bool IsDecription { get; set; }
            public static string DecriptionContent { get; set; }
        }
        public string Description_TB { get => _Description_TB; set { _Description_TB = value; OnPropertyChanged(); } }
        private string _Description_TB;
        public ICommand CloseWindowCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        public EventDecriptionViewModel()
        {
            Decription.IsDecription = false;
            CloseWindowCommand = new RelayCommand<Window>((p) => { return p != null; }, (p) => { CloseWindow(p); });
            CheckCommand = new RelayCommand<Window>(
                (p) => { return Description_TB != null; },
                (p) => { 
                    Decription.IsDecription = true; 
                    Decription.DecriptionContent = Description_TB;
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

