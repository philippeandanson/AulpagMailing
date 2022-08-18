using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace AulpagMailing.Helpers
{
    class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            try
            {
                
                DateTime DateEnvoi = (DateTime)value;
                if (DateEnvoi.Ticks == 0) return "/Images/erreur.png"; else return "/Images/check.png";
            }
            catch
            {
                return "/Images/erreur.png";
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EchecConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime DateEnvoi = (DateTime)value;
            if (DateEnvoi.Ticks == 0) return "Echec envoi"; else return DateEnvoi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
