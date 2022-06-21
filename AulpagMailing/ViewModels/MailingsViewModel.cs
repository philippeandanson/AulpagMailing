using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace AulpagMailing.ViewModels
{


    public  class MailingsViewModel : INotifyPropertyChanged
    {

        #region Définition des commandes
        public RelayCommand SaveCurrentEmailCommand  { get; }
        public RelayCommand ValidationCommand        { get; }
        public RelayCommand CloseWindowCommand       { get; }
        public RelayCommand SignCommand              { get; }
        public RelayCommand CloseWindow2Command      { get; }
        public RelayCommand ImportToCsvCommand       { get; }
        public RelayCommand ExportToCsvCommand       { get; }
        public RelayCommand ModifFicheDestinataire   { get; }
        public RelayCommand SendCommand              { get; }
        public RelayCommand PjCommand                { get; }
        public RelayCommand ListCommand              { get; }
        public RelayCommand NewDestinataireCommand   { get; }
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
        public RelayCommand AddSmtpCommand           { get; }
        public RelayCommand DeleteSmtpCommand        { get; }

        public ICommand SmtpSelectCommand 
        { 
            get { return new RelayCommand(x => CurrentSmtp = x as Smtp); }
        }
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
        public ObservableCollection<destinataires> ListDestinataires { get; set; }
        public ObservableCollection<Envoi>         ListEnvoi         { get; set; }      
        public CollectionView                      FiltreView        { get; set; }
        public ObservableCollection<mailings>      MailingsList      { get; set; }
        public ObservableCollection<pjs>           PiecesJointes     { get; set; }
        public ObservableCollection<Smtp>          ListSmtp          { get; set; }

        #region   Gestion des boutons
        public bool CourielChecked { get; set; } = true;
        public bool SmsChecked     { get; set; }
        private int selectedIdex;
        private mailings currentMailing;
        private bool closeTrigger2;
        private Boutons bt;
        private pjs currentPj;
        private Envoi currentListEnvoi;
        private Smtp currentSmtp;
       

        public Smtp CurrentSmtp
        {
            get
            {
                return currentSmtp;
            }
            set
            {
                currentSmtp = value;        
                SetSmtpActif();              
            }
        }
        public Envoi CurrentListEnvoi
        {
            get
            {
               
                return this.currentListEnvoi;
                
            }
            set
            {
                this.currentListEnvoi = value;
                if (ListEnvoi.Count == 0) Bt.Onglet3IsVisible = "Hidden";
                OnPropertyChanged(nameof(CurrentListEnvoi));
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
                return selectedIdex;
            }
            set
            {
                selectedIdex = value;
                if (selectedIdex == 0)
                {   if (!string.IsNullOrEmpty(CurrentMailing.objet_mailing)) bt.Onglet3IsVisible = "Visible"; else bt.Onglet3IsVisible = "Hidden";
                    GetListEnvoi();
                }
               else               
                {
                    if (!string.IsNullOrEmpty(CurrentMailing.objet_mailing))
                    {
                        // Mise à jour BDD
                        int key = Database.UpdateMailing(CurrentMailing);   // Récupère la clé pour les nouveaux enregistrements
                        SaveDossierListEnvoi(key);
                    }

                    if (CurrentMailing.date_envoi.Ticks == 0)  Bt.IsEnvoye = "Visible";else Bt.IsEnvoye = "Hidden";
                    //--------------------------------------
                    Bt.Onglet3IsVisible = "Hidden";
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
                AffichePiece();                         // Affichage des pièces jointes
                OnPropertyChanged(nameof(CurrentMailing));
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
        #endregion

        public MailingsViewModel()
        {

         #region initialisation
            bt = new Boutons();
            bt.Fiche_Selectionnes = true;
            bt.Recherche = "";
            ListDestinataires = Database.GetDestinataire();
            ListSmtp = Database.GetSmtp;
            FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
            FiltreView.Filter = Contains;
            CurrentMailing = new mailings();
            CurrentSmtp = (Smtp)Database.GetSmtp.FirstOrDefault(x => x.actif == true); // Récupère le Smtp actif
            MailingsList = Database.ReadMailing;
            ListEnvoi = new ObservableCollection<Envoi>();
            Bt.Onglet3IsVisible = "Hidden";
            #endregion

         AddSmtpCommand           = new RelayCommand(x =>
            {
                CurrentSmtp = new Smtp();
                OnPropertyChanged("CurrentSmtp");
                OnPropertyChanged("ListSmtp");

            });
         DeleteSmtpCommand        = new RelayCommand(x =>
            {
                ListSmtp.Remove(CurrentSmtp);
                Database.DeleteSmtp(CurrentSmtp);
                OnPropertyChanged("CurrentSmtp");
                OnPropertyChanged("ListSmtp");

            });
         UpdateSmtpCommand        = new RelayCommand(x =>
            {
                ListSmtp.Add(CurrentSmtp);
                SetSmtpActif();

            });
         DeleteDossierCommand     = new RelayCommand(x =>
         {
             if (x == null) return;

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
                        if (bt.Usager && item.categorie == 1 && item.adherent)
                            item.selected = false;
                        if (bt.Personalite && item.categorie == 2 && item.adherent)
                            item.selected = false;
                        if (bt.Presse && item.categorie == 3 && item.adherent)
                            item.selected = false;
                    }

                    if (bt.N_inscrit)
                    {
                        if (bt.Usager && item.categorie == 1 && !item.adherent)
                            item.selected = false;
                        if (bt.Personalite && item.categorie == 2 && !item.adherent)
                            item.selected = false;
                        if (bt.Presse && item.categorie == 3 && !item.adherent)
                            item.selected = false;
                    }
                }

            });
         AnnulDestinataireCommand = new RelayCommand(x =>
            {
                ListEnvoi.Remove(CurrentListEnvoi);
                OnPropertyChanged(nameof(ListEnvoi));

            });
         ReselectionCommand       = new RelayCommand(x =>
            {
                              
                    foreach (var item in ListDestinataires)
                    {

                    if (bt.Inscrit)
                    {

                            if (bt.Usager && item.categorie == 1 && item.adherent)
                                item.selected = true;
                            if (bt.Personalite && item.categorie == 2 && item.adherent)
                                item.selected = true;
                            if (bt.Presse && item.categorie == 3 && item.adherent)
                                item.selected = true;
                        }

                    if (bt.N_inscrit)
                    {
                            if (bt.Usager && item.categorie == 1 && !item.adherent)
                                item.selected = true;
                            if (bt.Personalite && item.categorie == 2 && !item.adherent)
                                item.selected = true;
                            if (bt.Presse && item.categorie == 3 && !item.adherent)
                                item.selected = true;
                        }
                    }
     
            });
         ListCommand              = new RelayCommand(x =>
            {
                if (!string.IsNullOrEmpty(CurrentMailing.objet_mailing)) SaveDossier();

                CloseTrigger2 = false;
                new DocumentsList(this).ShowDialog();
                foreach (var item in ListDestinataires) item.selected = false;
                OnPropertyChanged(nameof(ListDestinataires));
                GetEmailFromList();
                if (CurrentMailing.date_envoi.Ticks == 0) Bt.IsEnvoye = "Visible"; else Bt.IsEnvoye = "Hidden";
            });
         SaveCurrentEmailCommand  = new RelayCommand(x =>
            {
                if (string.IsNullOrEmpty(CurrentMailing.objet_mailing))
                {
                    MessageBox.Show("Vous n'avez pas enregistré d'objet");
                }
                else
                {
                    SaveDossier();
                    MessageBox.Show("Mise à jour enregistrée", "Enregistrement");
                }

            });
         ValidationCommand        = new RelayCommand(x =>
             {
                 CurrentMailing = x as mailings;
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
                 OnPropertyChanged(nameof(CourielChecked));
                 OnPropertyChanged(nameof(SmsChecked));             
                 CloseTrigger2 = true;
             });
         SignCommand              = new RelayCommand(x =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
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
              
                    OpenFileDialog dialog = new OpenFileDialog();
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
         CloseWindow2Command      = new RelayCommand(x =>
            {
                CloseTrigger2 = true;
                CurrentMailing = new mailings();
                CurrentMailing.objet_mailing = "Nouveau document";
                OnPropertyChanged("CurrentMailing");
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
         SendCommand              = new RelayCommand(x =>
            {
                
                ListEnvoi.Clear();
                ListEnvoi = Database.GetEnvoiMail(CurrentMailing.id_mailing);
                EnvoiMail.Mail(ListEnvoi, CurrentMailing,PiecesJointes,CurrentSmtp) ;
                CurrentMailing.date_envoi=DateTime.Now;
                Database.UpdateMailing(CurrentMailing);
                bt.Onglet3IsVisible = "Hidden";
            });
        }

        #region procédures     
        private bool Contains(object de)
        {

            destinataires destinataire = de as destinataires;
            bool t1 = destinataire.categorie == 1 && bt.Usager;
            bool t2 = destinataire.categorie == 2 && bt.Personalite;
            bool t3 = destinataire.categorie == 3 && bt.Presse;
            bool t4 = destinataire.adherent && bt.Inscrit;
            bool t5 = !destinataire.adherent && bt.N_inscrit;
            bool t6 = destinataire.nom.StartsWith(bt.Recherche);
            bool t7 = destinataire.selected && bt.Fiche_Selectionnes;
            bool t8 = bt.Fiche_Selectionnes;

            return ((t7 || ((t1 || t2 || t3) && (t4 || t5) && t6)) && !t8) || t7;
        }   // Filtre les destinataires
        private void SaveDossier()
        {

            if (!string.IsNullOrEmpty(CurrentMailing.objet_mailing)  && CurrentMailing.date_envoi.Ticks==0)
              {
               // CourielChecked is true => type_mailing - 1=mail 2 = Sms;
                CurrentMailing.type_mailing = CourielChecked == true ? 1 : 2;
                CurrentMailing.date_creation = DateTime.Now;

                // Mise à jour BDD
                int key = Database.UpdateMailing(CurrentMailing);   // Récupère la clé pour les nouveaux enregistrements
                SaveDossierListEnvoi(key);
          
                //--------------------------------------

             }
            // raz Dossier courant / Liste des destinataires 
            CurrentMailing = new mailings();
            ListEnvoi.Clear();
            foreach (var item in ListDestinataires) item.selected = false;
            //------------

            //Recharge la liste des mail
            MailingsList = Database.ReadMailing;
           
            OnPropertyChanged(nameof(ListDestinataires));
            OnPropertyChanged(nameof(ListEnvoi));
        }
        private void SaveDossierListEnvoi(int key)
        {
            Database.DeleteLastPreparationEnvoi(key);
           
            foreach (var item in ListEnvoi)
            {
                var destinataire = Database.GetDestinataireById(item.id_destinataire);       // pour lecture fiche destinataire
                string contenu = "";
                if (!string.IsNullOrEmpty(CurrentMailing.contenu))            // Si le contenu n'est pas vide
                {
                    contenu = CurrentMailing.contenu.Replace("<nom>", destinataire.nom);  // récupère le nom
                    contenu = contenu.Replace("<prenom>", destinataire.prenom);                  // récupère le prénom
                }
                Task.Run(() =>
                {
                    item.fk_mailing = key;
                    item.contenu = RtfPipe.Rtf.ToHtml(contenu);         // Transforme le contenu en HTML
                                                                        // item.date_envoi = DateTime.Now;
                                                                        // EnvoiMail.Mail(ListEnvoi, CurrentMailing);
                    item.logo = CurrentMailing.signature;
                    Database.UpdateEnvoiFromEmail(item);                 // Sauvegarde dans la base
                    OnPropertyChanged("ListEnvoi");

                });
            }

        }
        private void GetEmailFromList()
        {
            ListEnvoi = Database.GetEnvoiMail(CurrentMailing.id_mailing);
            SaveListEnvoi = ListEnvoi.ToList();
            // Mise à jour liste des destinataires
            foreach (var item in ListEnvoi)
            {
                ListDestinataires.Where(X => X.id_destinataire == item.id_destinataire).FirstOrDefault().selected = true;
            }
            if (ListEnvoi.Count != 0) Bt.Onglet3IsVisible = "Visible";
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
                        contenu = CurrentMailing.contenu.Replace("<nom>", item.nom);
                        contenu = contenu.Replace("<prenom>", item.prenom);
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
                if (ListEnvoi.FirstOrDefault(y => y.id_destinataire == item.id_destinataire) != null)
                    item.selected = true;
                else item.selected = false;
            }

          

        }  //Mise à jour de la liste des destinataires sélectionnés
        private void AffichePiece()
        {
           PiecesJointes = Database.GetPiecesJointes(CurrentMailing);
           OnPropertyChanged(nameof(PiecesJointes));
        }
        private void SetSmtpActif()
        {
            foreach (var item in ListSmtp)
            {
                if (CurrentSmtp != null)
                {
                    if (item.host == CurrentSmtp.host) item.actif = true; else item.actif = false;
                    Database.UpdateSmtp(item);
                }
             
            }
            OnPropertyChanged("CurrentSmtp");
            OnPropertyChanged("ListSmtp");
        }
        public void SetParameter(mailings parameter)
        {
            CurrentMailing = parameter as mailings;           
            OnPropertyChanged("CurrentMailing");
        }     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));          
        }
     
        private void OnClosingCommandExecuted(CancelEventArgs cancelEventArgs)
        {

            OnPropertyChanged("CurrentMailing");
            if (!string.IsNullOrEmpty(CurrentMailing.objet_mailing))
            {
                int key = Database.UpdateMailing(CurrentMailing);   // Récupère la clé pour les nouveaux enregistrements
                SaveDossierListEnvoi(key);
                MessageBox.Show("Les données ont été sauvegardées");
            }
            else
            {

                if (!string.IsNullOrEmpty(CurrentMailing.contenu))
                {
                    MessageBoxResult result = MessageBox.Show("Si vous voulez sauvegarder le sujet vous devez remplir le champ sujet", "My App", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {

                        cancelEventArgs.Cancel = true;
                        return; 
                    }                   
                }
            }
            cancelEventArgs.Cancel = false;        
        }
        private void OnClosingCommandExecuted2(CancelEventArgs cancelEventArgs)
        {
            if (ListEnvoi.Count == 0) Bt.Onglet3IsVisible = "Hidden"; 
            else 
                Bt.Onglet3IsVisible = "Visible";
            cancelEventArgs.Cancel = false;
        }

        #endregion

    }
}

