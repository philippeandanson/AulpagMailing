using AulpagMailing.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

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



            //       FontFamilyCombo.ItemsSource = Fonts.SystemFontFamilies.OrderBy(fontFamily => fontFamily.Source);

            FontSizeCombo.Items.Add("8");
            FontSizeCombo.Items.Add("9");
            FontSizeCombo.Items.Add("10");
            FontSizeCombo.Items.Add("11");           
            FontSizeCombo.Items.Add("12");
            FontSizeCombo.Items.Add("14");
            FontSizeCombo.Items.Add("16");         
            FontSizeCombo.Items.Add("18");
            FontSizeCombo.Items.Add("20");
            FontSizeCombo.Items.Add("22");
            FontSizeCombo.Items.Add("24");
            FontSizeCombo.Items.Add("28");
            FontSizeCombo.Items.Add("36");
            FontSizeCombo.Items.Add("42");
            FontSizeCombo.Items.Add("72");
            FontSizeCombo.SelectedIndex = 0;
        //    FontFamilyCombo.SelectedIndex = 0;

           



        } 
    }

   
}
