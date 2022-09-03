using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AulpagMailing.ViewModels
{
    public partial class  MainWindowViewModel: INotifyPropertyChanged
	{

        #region variables envoi des emails     
        public mailings CurrentMailing { get; set; }
        public ObservableCollection<Envoi> ListEnvoi { get; set; }
        public ObservableCollection<Envoi> Journal { get; set; }
        // ProgressBar  Gerer le type de l'indicateur
        private bool isIndeterminate;
        public bool IsIndeterminate
        {
            get { return isIndeterminate; }
            set { isIndeterminate = value;OnPropertyChanged("IsIndeterminate"); }
        }
        private double pgb;
        public double Pgb
        {
            get { return pgb; }
            set
            {
                pgb = value;
                OnPropertyChanged("Pgb");
            }
        }
        #endregion

        #region Commande
        public RelayCommand EmailCommand      { get; }
        public RelayCommand InscritsCommand   { get; }
        public RelayCommand SmtpCommand       { get; }
        public RelayCommand ParametresCommand { get; }
        public RelayCommand PresseCommand     { get; }
        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(
                    (args) =>
                    {
                        Application.Current.Shutdown();
                    });
            }
        }
        #endregion

        #region variables
        public string Version { get; set; }
        private BackgroundWorker worker;
        private bool isLoading = false;
        public bool IsLoading
            {
                get
                {
                    return isLoading;
                }
                set
                {
                    isLoading = value;
                    OnPropertyChanged("IsLoading");
                   
                }
            }
        private string textCollecte;
        public string TextCollecte 
        { get { return textCollecte; } set { textCollecte = value; OnPropertyChanged("TextCollete"); } }
        private string progressBarColor = "Brown";
        public string ProgressBarColor
        { get { return progressBarColor; } set { progressBarColor = value; OnPropertyChanged("ProgressBarColor"); } }
       
        #endregion

        public MainWindowViewModel()
		{
            var t = Database.GetFirstContact();
            if (t!=null && (t.collecte != null)) Version = "Dernière intégration " + t.collecte.Value.ToString("dd/MM/yyyy");           
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (sender, args) =>
            {
                int visibilitySetting = args.ProgressPercentage;
                IsLoading = visibilitySetting == 0 ? false : true;
            };
               
            Messenger.Default.Register<object>(this, (message) =>
            {
                CurrentMailing = message as mailings;            
                worker.DoWork += WorkerDoWork2;
                worker.RunWorkerAsync();
            });
            EmailCommand = new RelayCommand(x =>
			{
                ShowUnique2();
              //  new Mailing().ShowDialog();
			});
			InscritsCommand = new RelayCommand(x =>
			{               
                MessageBoxResult result = MessageBox.Show("Voulez-vous mettre à jour les inscrits ? ", "Info", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
                // IsLoading = true;
              
                worker.DoWork += WorkerDoWork;
                worker.RunWorkerAsync();


            });
            SmtpCommand = new RelayCommand(x =>
            {
                ShowUnique1();            
            });
            ParametresCommand = new RelayCommand(x =>
            {
                ShowUnique3();
            });
            PresseCommand = new RelayCommand(x =>
            {
                new Contacts().ShowDialog();
            });
		}

        #region procédures
        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            IsIndeterminate = true;

            if (!TestConnexionInternet.IsConnectedToInternet())
            {
                MessageBox.Show("Connexion internet défailllante");
                return;
            }

            worker.ReportProgress(1);//shows progress bar
                                    
            ProgressBarColor = "Red";
            OnPropertyChanged("ProgressBarColor");
            var t = Database.cnn;
            Task.Run(() => {              
                t.Open();
            });

            while (t.State != System.Data.ConnectionState.Open)
            {
                TextCollecte = t.State.ToString();
                OnPropertyChanged("TextCollecte");
            }


            TextCollecte = "Collecte des inscrits en cours.....";
            OnPropertyChanged("TextCollecte");
            ProgressBarColor = "Blue";
            OnPropertyChanged("ProgressBarColor");
            Thread.Sleep(1500);     //load data
            Database.UpdateAdherent();
            TextCollecte = "Collecte des adhésions.....";
            OnPropertyChanged("TextCollecte");
            ProgressBarColor = "Green";
            OnPropertyChanged("ProgressBarColor");
            Thread.Sleep(1500);     //load data
            Database.UpdateAdhesion();

            Database.cnn.Close();

            worker.ReportProgress(0);//hides progress bar
        }
        private void WorkerDoWork2(object sender, DoWorkEventArgs e)
        {
            var mode = App.EnvoiTest;
            IsIndeterminate = false;
            worker.ReportProgress(1);//shows progress bar 
            App.SmtpServer = EnvoiMail.Connexion;
            ObservableCollection<pjs> PiecesJointes = Database.GetPiecesJointes(CurrentMailing);
            ListEnvoi = Database.GetEnvoiMail(CurrentMailing.id_mailing);
            double denominateur = ListEnvoi.Count();
            double numerateur = 0;
            string termine = "Envois terminés";
            foreach (var envoi in ListEnvoi)
            {
                if (envoi.date_envoi.Ticks == 0)
                {
                    pgb = numerateur++ / denominateur; OnPropertyChanged("Pgb");
                    TextCollecte = envoi.email; OnPropertyChanged("TextCollecte");
                    string result = EnvoiMail.Mail(envoi, CurrentMailing, PiecesJointes);
                    if (result != "")
                    {
                        termine = "Erreur serveur smtp";
                        ProgressBarColor = "Red";
                        MessageBox.Show(result);
                        break;
                    }
                    envoi.date_envoi = DateTime.Now;
                    if(!App.EnvoiTest) Database.UpdateEnvoi(envoi);  // Si mode test on enregistre pas
                }

            }
            TextCollecte = termine; OnPropertyChanged("Libelle");
            Pgb = 1; OnPropertyChanged("Pgb");

            worker.ReportProgress(0);//shows progress bar

        }
        #endregion     

        #region Gestion des écrans
        private Parametres _instance1 = null;
        public void ShowUnique1()
        {
            if (_instance1 == null) _instance1 = new Parametres(); else _instance1.Focus();
            try { _instance1.Show(); }
            catch (Exception e)
            { _instance1 = new Parametres(); _instance1.Show(); }
        }
        private Mailing _instance2 = null;
        public void ShowUnique2()
        {
            if (_instance2 == null) _instance2 = new Mailing(); else _instance2.Focus();
            try { _instance2.Show(); }
            catch (Exception e)
            { _instance2 = new Mailing(); _instance2.Show(); }
        }
        private AutresParametres _instance3 = null;
        public void ShowUnique3()
        {
            if (_instance3 == null) _instance3 = new AutresParametres(); else _instance3.Focus();
            try { _instance3.Show(); }
            catch (Exception e)
            { _instance3 = new AutresParametres(); _instance3.Show(); }
        }
        public void SetParameter(mailings parameter)
        {
            CurrentMailing = parameter as mailings;
            ListEnvoi = Database.GetEnvoiMail(CurrentMailing.id_mailing);
            App.SmtpServer = EnvoiMail.Connexion;
            worker.RunWorkerAsync();

        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;		
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
