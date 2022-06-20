using System;
using System.Globalization;
using System.Windows.Data;

namespace AulpagMailing.Helpers
{

    public class Type_mailingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int type = (int)value;
            if (type == 1) return "Courriel"; else return "SMS";                  
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
