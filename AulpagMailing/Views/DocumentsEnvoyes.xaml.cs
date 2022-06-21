using AulpagMailing.ViewModels;
using System.Windows;

namespace AulpagMailing.Views
{
   
    public partial class DocumentsEnvoyes : Window
    {
        public DocumentsEnvoyes() { }

        public DocumentsEnvoyes(MailingsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }

    
}
