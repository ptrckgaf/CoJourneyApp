﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CoJourney.App.Converters
{
    public class StringToDateConvertor : IValueConverter
    {
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            return ((DateTime)value).ToString("dddd, dd MMMM yyyy HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime toReturn;
            if (DateTime.TryParse(value.ToString(), out toReturn))
                return toReturn;
            return null;
        }
    }
}
