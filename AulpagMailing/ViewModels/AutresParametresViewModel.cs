using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;

using static AulpagMailing.Services.EnvoiMail;

namespace AulpagMailing.ViewModels
{
    class AutresParametresViewModel : INotifyPropertyChanged
    {
        private string signature;
        private string piece1;
        private string piece2;
        public List<parametres> parametres { get; set; }
        public string copie_email { get; set; }
        public string position_font { get; set; }
        public string taille_font { get; set; }
        public string test_email { get; set; }
        public string Signature
        {
            get { return signature; }
            set { signature = value;
                OnPropertyChanged("Signature");
            } }
        public string Piece1
        {
            get { return piece1; }
            set
            {
                piece1 = value;
                OnPropertyChanged("Piece1");
            }
        }
        public string Piece2
        {
            get { return piece2; }
            set
            {
                piece2 = value;
                OnPropertyChanged("Piece2");
            }
        }

        public string CurrentFont { get; set; }

        public Action CloseAction      { get; set; }
    
        
        public FontFamily []   MFont     {get;set;}

        public RelayCommand UpdateParametresCommand { get; }
        public RelayCommand SignCommand { get; }
        public RelayCommand Piece1Command { get; }
        public RelayCommand Piece2Command { get; }
        public RelayCommand PjCommand { get; }

        public AutresParametresViewModel()
        {
            parametres = Database.GetParametres;

            var installedFontCollection = new System.Drawing.Text.InstalledFontCollection();    
            MFont = installedFontCollection.Families;
     

            foreach (var item in parametres)
            {
                switch(item.id_parametre)
                {
                    case 1: copie_email   = item.parametre; break;
                    case 2: position_font = item.parametre; break;
                    case 3: taille_font   = item.parametre; break;
                    case 4: test_email    = item.parametre; break;
                    case 5: signature     = item.parametre; break;
                    case 6: piece1        = item.parametre; break;
                    case 7: piece2        = item.parametre; break;
                }
          
            }                     
            UpdateParametresCommand = new RelayCommand(x =>
            {               
                if(!CheckEmail.IsEmail(copie_email))
                {
                    System.Windows.MessageBox.Show("Adresse mail erronnée");
                    return;
                }
                
                parametres = new List<parametres>();
                parametres.Add(new Models.parametres { id_parametre = 1, parametre = copie_email });
                parametres.Add(new Models.parametres { id_parametre = 2, parametre = position_font });
                parametres.Add(new Models.parametres { id_parametre = 3, parametre = taille_font });
                parametres.Add(new Models.parametres { id_parametre = 4, parametre = test_email });
                parametres.Add(new Models.parametres { id_parametre = 5, parametre = signature });
                parametres.Add(new Models.parametres { id_parametre = 6, parametre = piece1 });
                parametres.Add(new Models.parametres { id_parametre = 7, parametre = piece2 });

                foreach (var item in parametres)
                {
                    Database.UpdateParametres(item);
                }
                CloseAction();
            });
            SignCommand = new RelayCommand  (x =>
            {
            
            OpenFileDialog dialog = new OpenFileDialog();          
            bool? result = dialog.ShowDialog();
            if (result == true)
                {                 
                    Signature = dialog.FileName;                                                  
                }

            });
            Piece1Command = new RelayCommand(x =>
            {

                using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dlg.Description = "Sélection d'un répertoire";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Piece1 = dlg.SelectedPath;
                      
                    }
                }


            });
            Piece2Command = new RelayCommand(x =>
            {
                using (System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dlg.Description = "Sélection d'un répertoire";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Piece2 = dlg.SelectedPath;

                    }
                }
            });
        }

     

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
