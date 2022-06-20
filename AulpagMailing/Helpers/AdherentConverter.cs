using System;
using System.Globalization;
using System.Windows.Data;

namespace AulpagMailing.Helpers
{
    class AdherentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool adherent = (bool)value;
            if (adherent == true) return "Oui"; else return "Non";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
