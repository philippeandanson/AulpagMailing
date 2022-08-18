using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.Views;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Mail;
using AulpagMailing.Helpers;
using GalaSoft.MvvmLight.Messaging;

namespace AulpagMailing.ViewModels
{
    public  class MailingsViewModel :  INotifyPropertyChanged,IMessageSender
    {
        #region Définition des commandes     
     
        public RelayCommand CloseWindowCommand       { get; }
        public RelayCommand SignCommand              { get; }
        public RelayCommand CloseWindow2Command      { get; }
        public RelayCommand ImportToCsvCommand       { get; }
        public RelayCommand ExportToCsvCommand       { get; }
        public RelayCommand ModifFicheDestinataire   { get; } 
        public RelayCommand PjCommand                { get; }
        public RelayCommand NouveauMailCommand       { get; }
        public RelayCommand ListCommand              { get; }
        public RelayCommand ValidationCommand { get; }
        public RelayCommand NewDestinataireCommand   { get; }
        public RelayCommand SendCommand { get; }
        public RelayCommand SendCommand2 { get; }
        public RelayCommand PrepareCommand           { get; }
        public RelayCommand SignatureCommand         { get; }
        public RelayCommand ListMailingsCommand      { get; }
        public RelayCommand DeselectionCommand       { get; }
        public RelayCommand AnnulDestinataireCommand { get; }
        public RelayCommand ReselectionCommand       { get; }
        public RelayCommand DeletePieceCommand       { get; }
        public RelayCommand DeleteLogoCommand        { get; }
        public RelayCommand DeleteDossierCommand     { get; }
        public RelayCommand UpdateSmtpCommand        { get; }          
        public RelayCommand DeleteSmtpCommand        { get; }
        public RelayCommand OpenOngle3Command        { get; }
        public ICommand WindowClosing2
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted2);      
            }
        }
        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);
            }
        }
                     
        #endregion

        private List<Envoi> SaveListEnvoi = new List<Envoi>();      // Liste des envois pour sauvegatde
        private ObservableCollection<mailings> mailingsList;
        public ObservableCollection<destinataires> ListDestinataires { get; set; }
        public ObservableCollection<Envoi>         ListEnvoi         { get; set; }      
        public CollectionView                      FiltreView        { get; set; }       
        public ObservableCollection<mailings>      MailingsList
        {
            get { return mailingsList; }
            set { mailingsList = value; OnPropertyChanged(nameof(MailingsList)); }
        }
        public ObservableCollection<pjs>           PiecesJointes     { get; set; }
        public ObservableCollection<Smtp>          ListSmtp          { get; set; }

        #region   Gestion des boutons
        public bool CourielChecked { get; set; } = true;
        public bool SmsChecked     { get; set; }
        private string messages = "Ce mail a été intégralement diffusé";
        public string Messages { get { return messages; } set { messages = value; OnPropertyChanged("Messages"); } } 
        private int selectedIndex;
        private mailings currentMailing;
        private bool closeTrigger2;
        private Boutons bt;
        private pjs currentPj;
        private Envoi currentListEnvoi;     
        private string isSending = "Hidden";
    
        public Envoi CurrentListEnvoi
        {
            get
            {
               
                return this.currentListEnvoi;
                
            }
            set
            {
                this.currentListEnvoi = value;
                OnPropertyChanged("CurrentListEnvoi");
               
            }
        }
        public pjs CurrentPj
        { 
            get
            {
                return this.currentPj;
            }
            set
            {
                this.currentPj = value;
                OnPropertyChanged(nameof(CurrentPj));
            }
        }
        public int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                if (selectedIndex == 0)
                {
                    SaveDossierListEnvoi();
                    if (ListDestinataires.Where(x => x.selected == true).Count() != 0)
                        Bt.Onglet3IsVisible = "visible";
                    else
                        Bt.Onglet3IsVisible = "Hidden";
                }
                else   
                {
     
                    if (CurrentMailing.date_envoi.Ticks == 0)  Bt.IsEnvoye = "Visible";else Bt.IsEnvoye = "Hidden";
                    //--------------------------------------
                 
                    Bt.Fiche_Selectionnes = false;
                    GetDestinatairesSelection();
                    GetListEnvoi();
                    FiltreView.Filter = Contains;
                    FiltreView.Refresh();                  
                }

                OnPropertyChanged(nameof(ListDestinataires));
                OnPropertyChanged(nameof(ListEnvoi));
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public mailings  CurrentMailing 
        {
            get 
            { 
                return currentMailing;
            }
            set
            {
                currentMailing = value;
                OnPropertyChanged(nameof(CurrentMailing));
                AffichePiece();                         // Affichage des pièces jointes
            
              
            }
         }       
        public bool CloseTrigger2
        {
            get
            {
                return this.closeTrigger2;
            }
            set
            {
                this.closeTrigger2 = value;
                OnPropertyChanged(nameof(CloseTrigger2));
            }
        }     
        public Boutons Bt
        {
            get
            {
                FiltreView.Filter = Contains;
                FiltreView.Refresh(); return bt;
            }
            set
            {
                bt = value;
                FiltreView.Filter = Contains;
                FiltreView.Refresh();
            }
        }      
        public string IsSending
        {
            get
            {
                return isSending;
            }
            set
            {
                isSending = value;
                OnPropertyChanged("IsSending");

            }
        }
        private bool onglet2Enabled;
        public  bool Onglet2Enabled
        {get 
            { 
                return onglet2Enabled; 
            } 
            set {
                onglet2Enabled = value;
                OnPropertyChanged("Onglet2Enabled"); 
            } }
        private bool isZoneEnabled=false;
        public bool IsZoneEnabled
        { get { return isZoneEnabled; }set { isZoneEnabled = value;OnPropertyChanged("IsZoneEnabled"); } }
        private string envoiMailTermine="Hidden";
        public string EnvoiMailTermine
        {
            get
            {
                return envoiMailTermine;
            }
            set
            {
                envoiMailTermine = value;
                OnPropertyChanged("EnvoiMailTermine");
            }
        }

        #endregion

        public MailingsViewModel()
        {
            #region initialisation
            App.Staticparametres = Database.GetParametres;
            bt = new Boutons();
            bt.Usager = true;
            bt.Fiche_Selectionnes = true;
            bt.Recherche = "";
            IsZoneEnabled = false;
            ListDestinataires = Database.GetDestinataire();
            ListSmtp = Database.GetSmtp;
            FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
            FiltreView.Filter = Contains;
            CurrentMailing = new mailings();           
            ListEnvoi = new ObservableCollection<Envoi>();        
            Bt.Onglet3IsVisible = "Hidden";
            #endregion
            #region Commandes
   
             DeleteDossierCommand     = new RelayCommand(x =>
             {
                 if (x == null) return;
                 mailings mail = x as mailings;
                 MessageBoxResult result = MessageBox.Show("Voulez-vous supprimer cet e-mail ? ", "Info", MessageBoxButton.YesNo);
                 if (result != MessageBoxResult.Yes)
                 {
                     return;
                 }

                 // On be supprime pas de dossier quand il a eu des mails envoyés
                 if ( mail.envoye!=0 && mail.envoye != null) return;

                 Database.DeleteDossier(x as mailings);
                 Database.DeletePiecesJointes(x as pjs);
                 MailingsList.Remove(x as mailings);
            
                 CurrentMailing = new mailings();

             });
             DeletePieceCommand       = new RelayCommand(x =>
               {
                  Database.DeletePiecesJointes(CurrentPj);
                  PiecesJointes.Remove(CurrentPj);
                  if(ListEnvoi.Count==0) bt.Onglet3IsVisible= "Hidden";
               });
             DeleteLogoCommand        = new RelayCommand(x =>
                {
                   CurrentMailing.signature = null;
                   Database.UpdateMailing(CurrentMailing);
                   OnPropertyChanged(nameof(CurrentMailing));

                });                
             DeselectionCommand       = new RelayCommand(x =>
                { 
               
                    foreach(var item in ListDestinataires)
                    {
                        if (bt.Inscrit)
                        {
                            if (bt.Usager && item.categorie == 1)
                                item.selected = false;
                            if (bt.Personalite && item.categorie == 2 )
                                item.selected = false;
                            if (bt.Presse && item.categorie == 3 )
                                item.selected = false;
                            if (bt.Test && item.categorie == 4)
                                item.selected = false;
                        }

                        if (bt.N_inscrit)
                        {
                            if (bt.Usager && item.categorie == 1)
                                item.selected = false;
                            if (bt.Personalite && item.categorie == 2 )
                                item.selected = false;
                            if (bt.Presse && item.categorie == 3 )
                                item.selected = false;
                            if (bt.Test && item.categorie == 4)
                                item.selected = false;
                        }
                    }

                });
             AnnulDestinataireCommand = new RelayCommand(x =>
                {
                   
                    if (CurrentMailing.envoye != 0) return;  // Si aucun reste on sort
                    Envoi CurrentEnvoi = x as Envoi;        // L'envoi sélectionné
                    ListEnvoi.Remove(CurrentEnvoi);                 // Suppression objet dans liste
                    Database.DeleteCurrentEnvoi(CurrentEnvoi);      // Suppression objet dans la table
                    if (ListEnvoi.Count == 0) bt.Onglet3IsVisible = "Hidden";  // Si la liste est vide on ferme l'onglet envoi
                    OnPropertyChanged(nameof(ListEnvoi));

                });
             ReselectionCommand       = new RelayCommand(x =>
                {
                              
                        foreach (var item in ListDestinataires)
                        {

                        if (bt.Inscrit)
                        {

                                if (bt.Usager && item.categorie == 1 )
                                    item.selected = true;
                                if (bt.Personalite && item.categorie == 2 )
                                    item.selected = true;
                                if (bt.Presse && item.categorie == 3 )
                                    item.selected = true;
                                if (bt.Test && item.categorie == 4)
                                   item.selected = true;
                        }

                        if (bt.N_inscrit)
                        {
                                if (bt.Usager && item.categorie == 1 )
                                    item.selected = true;
                                if (bt.Personalite && item.categorie == 2)
                                    item.selected = true;
                                if (bt.Presse && item.categorie == 3 )
                                    item.selected = true;
                                if (bt.Test && item.categorie == 4)
                                    item.selected = true;
                        }
                        }
     
                });
             ListCommand              = new RelayCommand(x =>
                {

                    // Si Lobjet courrant n'est pas vide et qu'aucun mail n'a pas été envoyé and sauvegarde le dossier
                    if (
                        (CurrentMailing.id_mailing != 0 && CurrentMailing.total != null && CurrentMailing.envoye == 0) ||
                        (CurrentMailing.total == null && CurrentMailing.id_mailing != 0)
                    )
                    {
                        SaveDossier2();
                    }
                    MailingsList = Database.ReadMailing;                   
                    Bt.IsEnvoye = "Visible";
                    CloseTrigger2 = false;
                    CurrentMailing = new mailings();
                    DeselectionContacts();
                    new DocumentsList(this).ShowDialog();
                    foreach (var item in ListDestinataires) item.selected = false;
                    OnPropertyChanged(nameof(ListDestinataires));
                    GetEmailFromList();
              
                });         
             ValidationCommand        = new RelayCommand(x =>
                 {
                     CurrentMailing = x as mailings;

                     if(CurrentMailing.reste!=0)
                     {
                         Messages= "Ce mail a été diffusé partiellement";
                        
                     }else
                     { Messages = "Ce mail a été totalement diffusé"; }
                    
                     if (CurrentMailing.type_mailing == 1)
                     {
                         CourielChecked = true;
                         SmsChecked = false;
                     }
                     else
                     {
                         CourielChecked = false;
                         SmsChecked = true;
                     }
                     if (
                           (CurrentMailing.id_mailing != 0 && CurrentMailing.total != null && CurrentMailing.envoye == 0) ||
                           (CurrentMailing.total == null && CurrentMailing.id_mailing != 0)
                       )
                     { 
                         Onglet2Enabled = true; EnvoiMailTermine = "Hidden";
                         IsZoneEnabled = true; 
                     }
                     else
                     { Onglet2Enabled = false; EnvoiMailTermine = "Visible"; IsZoneEnabled = false; }                      
                          
                     CloseTrigger2 = true;
                     
                 });
             SignCommand              = new RelayCommand(x =>
                {
                    string dir = Path.GetDirectoryName( Database.GetLastMailing.signature);                   
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.InitialDirectory = dir;
                    bool? result = dialog.ShowDialog();
                        if (result == true)
                        {
 
                        // Open document 
                            CurrentMailing.signature = dialog.FileName;
                            OnPropertyChanged("CurrentMailing");
                        }
            
                        });        
             PjCommand                = new RelayCommand(x =>
                {

                        string dir = Path.GetDirectoryName(Database.GetLastPjs.piece);
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.InitialDirectory = dir;
                        dialog.Multiselect = true;
                        dialog.ShowDialog();
                        string[] result = dialog.FileNames;
                        pjs piece = new pjs();
                        foreach (string y in result)
                        {
                            piece.fk_mailing = CurrentMailing.id_mailing;
                            piece.piece = y;
                            // On enregistre pas les pièces dans la base si on a pas de n°id de la table mail
                            if(piece.fk_mailing!=0)  Database.UpdatePjAsync(piece);
                            PiecesJointes.Add(piece);
                        }
              
                }); 
             NouveauMailCommand       = new RelayCommand(x =>
                {
                    CloseTrigger2 = true;                  
                    CurrentMailing = new mailings();
                    CurrentMailing.signature = Database.GetLastMailing.signature;
                    CurrentMailing.objet_mailing = "Nouveau document";
                    OnPropertyChanged("CurrentMailing");
                    Onglet2Enabled = true;
                    IsZoneEnabled=true;
                    SaveDossier2();

                });
             ImportToCsvCommand       = new RelayCommand(x =>
                {
                   // IEnumerable<Destinataires_export> list;
                    OpenFileDialog dialog = new OpenFileDialog();
                    Nullable<bool> result = dialog.ShowDialog();
                    if (result == true)
                    {                   
                       IEnumerable<Destinataires_export>  list = ImportExportData.ReadCSV(dialog.FileName);
                       Database.ImportDestinataire(list);
                        ListDestinataires = Database.GetDestinataire();
                        FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
                        FiltreView.Filter = Contains;
                        OnPropertyChanged(nameof(ListDestinataires));
                        MessageBox.Show("Importation terminée");
                    }             
                }); 
             ExportToCsvCommand       = new RelayCommand(x =>
                {             
                  ImportExportData.WriteCSVFile(Database.GetDestinataires);
                  MessageBox.Show("Exportation terminée");
                });
             ModifFicheDestinataire   = new RelayCommand(x =>
                {              
                    new FicheDestinataire(x as destinataires).ShowDialog();
                    ListDestinataires = Database.GetDestinataire();
                    FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
                    FiltreView.Filter = Contains;
                    OnPropertyChanged(nameof(ListDestinataires));
                });
             NewDestinataireCommand   = new RelayCommand(x =>
                {             
                    new FicheDestinataire(new destinataires()).ShowDialog();
                    ListDestinataires = Database.GetDestinataire();
                    FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
                    FiltreView.Filter = Contains;
                    OnPropertyChanged(nameof(ListDestinataires));
                });
             SendCommand              = new RelayCommand( x =>
            {
               
                 Messenger.Default.Send<object>(CurrentMailing);
              
               // new SendMails(CurrentMailing).Show();                 
                CurrentMailing = new mailings();

                #region Réinitialise l"ecran "EnvoiMail"
                CurrentMailing = new mailings();
                DeselectionContacts();
                ListEnvoi.Clear();
                OnPropertyChanged("ListEnvoi");
                SelectedIndex = 0;
                #endregion

            });
  
            #endregion
        }

        #region procédures     
        private bool Contains(object de)
        {

            destinataires destinataire = de as destinataires;
            bool t1 = destinataire.categorie == 1 && bt.Usager;
            bool t2 = destinataire.categorie == 2 && bt.Personalite;
            bool t3 = destinataire.categorie == 3 && bt.Presse;
            bool t4 = destinataire.categorie == 4 && bt.Test;

            bool t6 = destinataire.nom.StartsWith(bt.Recherche);
            bool t7 = destinataire.selected && bt.Fiche_Selectionnes;
            bool t8 = bt.Fiche_Selectionnes;

            return ((t7 || ((t1 || t2 || t3 || t4)  && t6)) && !t8) || t7  || (t6 && !string.IsNullOrEmpty(bt.Recherche));
        }   // Filtre les destinataires
        private void SaveDossier2()
        {
           
                // CourielChecked is true => type_mailing - 1=mail 2 = Sms;
                CurrentMailing.type_mailing = CourielChecked == true ? 1 : 2;
                CurrentMailing.date_creation = DateTime.Now;
                // Mise à jour BDD
                 Database.UpdateMailing(CurrentMailing);   // Récupère la clé pour les nouveaux enregistrements
                //SaveDossierListEnvoi();

                //--------------------------------------          
    
        }  
        private void SaveDossierListEnvoi()
        {
            List<destinataires> Contacts = ListDestinataires.Where(x=>x.selected ==true).ToList();
            if (CurrentMailing.envoye!=null && CurrentMailing.envoye != 0) return;
            // Suppression de la liste envoi des contacts non envoyés
            Database.DeleteLastPreparationEnvoi(CurrentMailing.id_mailing);
           
            Task.Run(() =>
           {

           foreach (var item in Contacts)
            {
                
              
                string contenu = "";
                if (!string.IsNullOrEmpty(CurrentMailing.contenu))                            // Si le contenu n'est pas vide
                {
                    contenu = CurrentMailing.contenu.Replace("<nom>", item.nom);      // récupère le nom
                    contenu = contenu.Replace("<prenom>", item.prenom);               // récupère le prénom
                    contenu = contenu.Replace("<civilite>", item.civilité);          // récupère la civilité
                }

                Envoi courrier = new Envoi();
              
                    courrier.fk_mailing = CurrentMailing.id_mailing;
                    courrier.id_destinataire = item.id_destinataire;
                    courrier.email = item.email;
                    courrier.contenu = RtfPipe.Rtf.ToHtml(contenu);         // Transforme le contenu en HTML                  
                    courrier.logo = CurrentMailing.signature;
                    Database.UpdateEnvoiFromEmail(courrier);                // Sauvegarde dans la base             
                }

             ListEnvoi = Database.GetEnvoiMail(CurrentMailing.id_mailing);
               OnPropertyChanged("ListEnvoi");
           });

        }
        private void GetEmailFromList()
        {
            if (CurrentMailing.id_mailing == 0) return;
            ListEnvoi = Database.GetEnvoiMail(CurrentMailing.id_mailing);
            SaveListEnvoi = ListEnvoi.ToList();
            // Mise à jour liste des destinataires
            foreach (var item in ListEnvoi)
            {               
                ListDestinataires.Where(X => X.id_destinataire == item.id_destinataire).FirstOrDefault().selected = true;
            }

          
            if (CurrentMailing.id_mailing !=0 && CurrentMailing.reste != 0  && CurrentMailing.total !=null) 
            {
                SelectedIndex = 2; 
            }
            OnPropertyChanged(nameof(ListEnvoi));
        }    
        private void GetListEnvoi()
        {
            ListEnvoi.Clear();
            OnPropertyChanged("ListDestinataires");
            foreach (var item in ListDestinataires)
            {
                string contenu = "";
                if (item.selected)
                {
                    if (CurrentMailing.contenu != null)   // pour le cas d'un nouveau mail si aucun contenu
                    {

                        contenu = CurrentMailing.contenu.Replace("<nom>", EncodeCharacters(item.nom));
                        contenu = contenu.Replace("<civilite>", EncodeCharacters(item.civilité));                        
                        contenu = contenu.Replace("<prenom>", EncodeCharacters(item.prenom));                        
                    }
                    else contenu = "";

                    var temp = new Envoi
                    {
                        id_destinataire = item.id_destinataire,
                        fk_mailing = CurrentMailing.id_mailing,
                        email = item.email,
                        contenu = contenu,
                        logo = CurrentMailing.signature
                        
                    };

                    ListEnvoi.Add(temp);
         
                }
            }
         
            OnPropertyChanged(nameof(ListEnvoi));
        }
        private void GetDestinatairesSelection()
        {
            foreach (var item in ListDestinataires)
            {
                var  liste = ListEnvoi.FirstOrDefault(y => y.id_destinataire == item.id_destinataire);
                if (liste!=null && liste.date_envoi.Ticks==0  ) item.selected = true;
                else item.selected = false;
            }       

        }  //Mise à jour de la liste des destinataires sélectionnés
        private void AffichePiece()
        {
           PiecesJointes = Database.GetPiecesJointes(CurrentMailing);
           OnPropertyChanged(nameof(PiecesJointes));
        }
        private void DeselectionContacts()
        {
            foreach (var item in ListDestinataires)
            {


                item.selected=false;
/*
                if (bt.Inscrit)
                {
                    if (bt.Usager && item.categorie == 1)
                        item.selected = false;
                    if (bt.Personalite && item.categorie == 2)
                        item.selected = false;
                    if (bt.Presse && item.categorie == 3)
                        item.selected = false;
                }

                if (bt.N_inscrit)
                {
                    if (bt.Usager && item.categorie == 1)
                        item.selected = false;
                    if (bt.Personalite && item.categorie == 2)
                        item.selected = false;
                    if (bt.Presse && item.categorie == 3)
                        item.selected = false;
                }
*/
            }

        }
      
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));          
        }   
        private void OnClosingCommandExecuted(CancelEventArgs cancelEventArgs)
        {

            OnPropertyChanged("CurrentMailing");
            if (CurrentMailing.id_mailing!=0 && CurrentMailing.reste!=0)
            {
                int key = Database.UpdateMailing(CurrentMailing);   // Récupère la clé pour les nouveaux enregistrements
                SaveDossierListEnvoi();
               
            }
          
            cancelEventArgs.Cancel = false;        
        }
        private void OnClosingCommandExecuted2(CancelEventArgs cancelEventArgs)
        {
            if (CurrentMailing.id_mailing == 0) { EnvoiMailTermine = "Hidden"; IsZoneEnabled = false; Onglet2Enabled = false;  }
            if (CurrentMailing.reste == 0 || CurrentMailing.id_mailing==0 || CurrentMailing.total==null) 
            {
                Bt.Onglet3IsVisible = "Hidden";
                 SelectedIndex = 0;
           //     IsZoneEnabled = false;
            }
            else
                Bt.Onglet3IsVisible = "Visible";
            cancelEventArgs.Cancel = false;
        }
        private  string EncodeCharacters(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";

         
            return text
                .Replace("ë", @"\'eb")
                .Replace("î", @"\'ed")
                .Replace("ï", @"\'ef")
                .Replace("ç", @"\'e7")
                .Replace("Ç", @"\'c7")
                .Replace("É", @"\'c9")
                .Replace("é", @"\'e9")
                .Replace("è", @"\'e8")
                .Replace("ą", @"\'b9")
                .Replace("ć", @"\'e6")
                .Replace("ę", @"\'ea")
                .Replace("ł", @"\'b3")
                .Replace("ń", @"\'f1")
                .Replace("ó", @"\'f3")
                .Replace("ś", @"\'9c")
                .Replace("ź", @"\'9f")
                .Replace("ż", @"\'bf")
                .Replace("Ą", @"\'a5")
                .Replace("Ć", @"\'c6")
                .Replace("Ę", @"\'ca")
                .Replace("Ł", @"\'a3")
                .Replace("Ń", @"\'d1")
                .Replace("Ó", @"\'d3")
                .Replace("Ś", @"\'8c")
                .Replace("Ź", @"\'8f")
                .Replace("Ż", @"\'af");
        }
        #endregion

    }
}

