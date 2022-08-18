using AulpagMailing.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AulpagMailing.Views
{
    /// <summary>
    /// Logique d'interaction pour AutresParametres.xaml
    /// </summary>
    public partial class AutresParametres : Window
    {
        public AutresParametres()
        {
            InitializeComponent();   
           
            AutresParametresViewModel vm = new AutresParametresViewModel();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
           
        }
    }
}
