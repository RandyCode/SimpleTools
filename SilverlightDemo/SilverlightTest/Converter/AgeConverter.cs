﻿using System;
using System.Windows.Data;

namespace SilverlightTest.Converter
{
    public class AgeConverter : IValueConverter

    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DateTime.Now.Year - ((DateTime)value).Year;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
