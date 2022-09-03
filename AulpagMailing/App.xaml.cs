using AulpagMailing.Models;
using System.Collections.Generic;
using System.Net.Mail;
using System.Windows;

namespace AulpagMailing
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static  SmtpClient SmtpServer { get; set; }
        public static List<parametres> Staticparametres = new List<parametres>();
        public static bool EnvoiTest { get; set; }

    }
}
