using System;
using System.Globalization;
using System.Windows.Data;

namespace AulpagMailing.Helpers
{
    public class DateNulleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
             DateTime  DateEnvoi = (DateTime)value;
            if (DateEnvoi.Ticks == 0) return ""; else return DateEnvoi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
