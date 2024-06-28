using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Project1.ViewModel.LoginViewModel;

namespace Project1.ViewModel
{
    public class ForgotViewModel : BaseViewModel
    {
        private string _CauHoiBaoMat_TB;
        public string CauHoiBaoMat_TB { get => _CauHoiBaoMat_TB; set { _CauHoiBaoMat_TB = value; OnPropertyChanged(); } }
        private string _CauTraLoi_TB;
        public string CauTraLoi_TB { get => _CauTraLoi_TB; set { _CauTraLoi_TB = value; OnPropertyChanged(); } }
        public ICommand ConfirmCommand { get; set; }
        public ForgotViewModel()
        {
            CauHoiBaoMat_TB = SharedData.CaiHoiBaoMat;
            ConfirmCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                if (CauTraLoi_TB == SharedData.CauTraLoi)
                {
                    MessageBox.Show("Mật khẩu của bạn là: " + SharedData.MatKhau);
                    p.Close();
                }
                else
                {
                    MessageBox.Show("Câu trả lời không đúng");
                }
            });
        }
    }
}
