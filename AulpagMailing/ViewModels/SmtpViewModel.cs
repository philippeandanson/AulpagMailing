using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AulpagMailing.ViewModels
{
    class SmtpViewModel : INotifyPropertyChanged
    {
        private Smtp currentSmtp;
        private string etat;
        private string status;
        private string currentSmtpIsVisible;

        public Smtp CurrentSmtp
        {
            get
            {
                return currentSmtp;
            }
            set
            {
                currentSmtp = value;
                if (currentSmtp != null && currentSmtp.actif == false) SetSmtpActif();
                OnPropertyChanged("CurrentSmtp");
            }
        }
        public string Etat
        {
            get
            {
                return etat;
            }
            set
            {
                etat = value;
                OnPropertyChanged("Etat");
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public string CurrentSmtpIsVisible
        {
            get
            {
                return currentSmtpIsVisible;
            }
            set
            {
                currentSmtpIsVisible = value;
                OnPropertyChanged("CurrentSmtpIsVisible");
            }
        }
        private Smtp CurrentSmtpTemp = new Smtp();

        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);
            }
        }

        public ObservableCollection<Smtp> ListSmtp { get; set; }
        public RelayCommand AddSmtpCommand         { get; }
        public RelayCommand DeleteSmtpCommand      { get; }
        public RelayCommand UpdateSmtpCommand      { get; }
        public RelayCommand AnnulSmtpCommand       { get; }
        public RelayCommand ModifSmtpCommand       { get; }

        public SmtpViewModel()
        {
            ListSmtp = Database.GetSmtp;
            CurrentSmtp = ListSmtp.FirstOrDefault(x => x.actif == true);
            Status = ListSmtp.Count == 0 ?  "I": "S";
            CurrentSmtpIsVisible = "Hidden";

           AddSmtpCommand = new RelayCommand(x =>
            {
                Status = "N";              
                CurrentSmtpIsVisible = "Visible";
                Etat = "Add";
                CurrentSmtp = new Smtp(); 
                
            });
            ModifSmtpCommand = new RelayCommand(x =>
            {
                Status = "N";
                CurrentSmtpIsVisible = "Visible";
                Etat = "Modif";
          
                CurrentSmtpTemp.host = CurrentSmtp.host;
                CurrentSmtpTemp.compte = CurrentSmtp.compte;
                CurrentSmtpTemp.port = CurrentSmtp.port;
                CurrentSmtpTemp.mdp = CurrentSmtp.mdp;
            });
            DeleteSmtpCommand = new RelayCommand(x =>
            {        
                MessageBoxResult result = MessageBox.Show("Confirmer la suprression du Smtp ? " + "\n\r\n\r" + CurrentSmtp.host  + "\n\r" +CurrentSmtp.compte
                    , "Info", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }              
                Database.DeleteSmtp(CurrentSmtp);             
                ListSmtp.Remove(CurrentSmtp);
                Status = ListSmtp.Count == 0 ? "I" : "S";
            });
           UpdateSmtpCommand = new RelayCommand(x =>
            {               
                string reponse = ControlSaisieSmtp(CurrentSmtp);
                if (Etat == "Add")
                    ListSmtp.Add(CurrentSmtp);
               
                Database.UpdateSmtp(CurrentSmtp);
                Status = ListSmtp.Count == 0 ? "I" : "S";
                CurrentSmtpIsVisible = "Hidden";              

            });


            AnnulSmtpCommand = new RelayCommand(x =>
            {
                Status = "S";
                CurrentSmtpIsVisible = "Hidden";
                CurrentSmtp.host = CurrentSmtpTemp.host;
                CurrentSmtp.compte = CurrentSmtpTemp.compte;
                CurrentSmtp.port = CurrentSmtpTemp.port;
                CurrentSmtp.mdp = CurrentSmtpTemp.mdp;


            });

        }

        private string ControlSaisieSmtp(Smtp smtp_saisi)
        {
           string reponse=null;
           if (ListSmtp.Where(x => x.host == smtp_saisi.host && x.compte == smtp_saisi.compte).Count()!=0) reponse= "Ce Smpt est déjà enregistré";
           if (string.IsNullOrEmpty(CurrentSmtp.compte)) reponse = "Host ou Login non saisis";
            return reponse;
        }
        private void SetSmtpActif()
        {
            foreach (var item in ListSmtp)
            {
               
                if (CurrentSmtp != null)
                {
                    if (item.host == CurrentSmtp.host && item.compte == CurrentSmtp.compte)
                    {
                        item.port = CurrentSmtp.port;
                        item.actif = true;
                    }
                    else item.actif = false;
                    Database.UpdateSmtp(item);
                }

            }
            OnPropertyChanged("CurrentSmtp");
            OnPropertyChanged("ListSmtp");

        }
        private void OnClosingCommandExecuted(CancelEventArgs cancelEventArgs)
        {
            if (Status != "I" && Status != "S")
            {
                MessageBox.Show("Annuler l'opération en cours");
                cancelEventArgs.Cancel = true;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /*
        Status	                I	        S	       N	    M
	                        base vide	 standart	Nouveau   Modifier
        Nouveau serveur 	    x	        x	        	
        Annuler			                                X	    x
        Modifier				            x
        Supprimer		                    x		
        Valider			        x       	            x       x

         */

    }
}
