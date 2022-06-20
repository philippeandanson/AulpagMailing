using System;
using System.Globalization;
using System.Windows.Data;

namespace AulpagMailing.Helpers
{
    public class CategorieConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int adherent = (int)value;

            string reponse;

            switch (adherent)
            {
                case 1:
                    reponse =  "Usager";
                    break;
                case 2:
                   reponse = "Personnalité";
                    break;
                default:
                    reponse = "Presse";
                    break;

            }//fin du switch

            return reponse;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
