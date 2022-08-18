using AulpagMailing.ViewModels;
using System.Windows;

namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour Contacts.xaml
    /// </summary>
    public partial class Contacts : Window
    {
        public Contacts()
        {
            InitializeComponent();
            DataContext = new ContactsViewModel();
        }
    }
}
