using AulpagMailing.Interfaces;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.ViewModels;
using System.Windows;

namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour FicheDestinataire.xaml
    /// </summary>
    public partial class FicheDestinataire : Window
    {
        public FicheDestinataire()
        {
            InitializeComponent();
            DataContext = new FicheDestinataireViewModel();
        }

        public FicheDestinataire(destinataires parameter) : this()
        {
            var p = DataContext as Iparameter2;
            p?.SetParameter(parameter);
        }

    
    }
}
