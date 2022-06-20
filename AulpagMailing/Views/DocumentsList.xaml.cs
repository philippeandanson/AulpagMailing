using AulpagMailing.ViewModels;
using System.Windows;

namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour ListeDocument.xaml
    /// </summary>
    public partial class DocumentsList : Window
    {
        public DocumentsList(){ }

        public DocumentsList(MailingsViewModel  viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

    }
}
