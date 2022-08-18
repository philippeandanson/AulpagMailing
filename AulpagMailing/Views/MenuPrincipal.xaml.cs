using AulpagMailing.Data;
using AulpagMailing.Services;
using AulpagMailing.ViewModels;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;


namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        public MenuPrincipal()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
     
    }

}

