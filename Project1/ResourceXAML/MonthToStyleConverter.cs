using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Project1.ResourceXAML
{
    public class MonthToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Kiểm tra giá trị đầu vào có phải là một số nguyên không
            if (value is int month)
            {
                // Xác định Style dựa trên tháng được chọn
                if (int.TryParse(parameter.ToString(), out int buttonMonth) && month == buttonMonth)
                {
                    return Application.Current.FindResource("selectedButtonMonth");
                }
            }

            // Trả về Style mặc định nếu không có Style nào được ánh xạ
            return Application.Current.FindResource("buttonMonth");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
