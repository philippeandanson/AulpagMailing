using AulpagMailing.ViewModels;
using System;
using System.Windows;

namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public Parametres()
        {
            InitializeComponent();
            DataContext = new SmtpViewModel();
        } 
    }
}
