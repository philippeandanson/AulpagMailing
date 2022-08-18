
using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static AulpagMailing.Services.EnvoiMail;

namespace AulpagMailing.ViewModels
{
    class AutresParametresViewModel
    {
        public List<parametres> parametres          { get; set; }
        public string copie_email                   { get; set; }
        public string position_font                 { get; set; }
        public string taille_font                   { get; set; }
        public Action CloseAction                   { get; set; }

        public RelayCommand UpdateParametresCommand { get; }

        public AutresParametresViewModel()
        {

            parametres = Database.GetParametres;

            foreach (var item in parametres)
            {
                switch(item.id_parametre)
                {
                    case 1: copie_email   = item.parametre; break;
                    case 2: position_font = item.parametre; break;
                    case 3: taille_font   = item.parametre; break;
                }
          
            }
          
            

            UpdateParametresCommand = new RelayCommand(x =>
            {

                
                if(!CheckEmail.IsEmail(copie_email))
                {
                    MessageBox.Show("Adresse mail erronnée");
                    return;
                }
                

                parametres = new List<parametres>();
                parametres.Add(new Models.parametres { id_parametre = 1, parametre = copie_email });
                parametres.Add(new Models.parametres { id_parametre = 2, parametre = position_font });
                parametres.Add(new Models.parametres { id_parametre = 3, parametre = taille_font });

                foreach (var item in parametres)
                {
                    Database.UpdateParametres(item);
                }
                CloseAction();
            });
        }

   
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
