using System;
using System.Windows.Data;

namespace SilverlightTest.Converter
{
    public class LevelConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((int)value == 1 ? "普通员工" : (int)value == 2 ? "资深员工" : "主管");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
