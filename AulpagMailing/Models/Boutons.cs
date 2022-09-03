using System.ComponentModel;

namespace AulpagMailing.Models
{
    public class Boutons : INotifyPropertyChanged
    {
        private bool   modeReel;
        private bool   modeTest=true;
        private bool   tous_les_adherents=true;
        private bool   adherents_a_jour;
        private bool   adherents_non_a_jour;
        private bool   signatured;
        private bool   personalite;
        private bool   presse;
        private bool   test;
        private bool   inscrit = true;
        private bool   n_inscrit = true;
        private bool   fiche_Selectionnes;
        private bool   newMail = true;
        private string recherche;
        private string onglet3IsVisible;
        private string onglet4IsVisible;
        private string isEnvoye;
      
        public string Onglet3IsVisible
        { get { return onglet3IsVisible; } set { onglet3IsVisible = value; OnPropertyChanged(nameof(Onglet3IsVisible)); } }
        public string Onglet4IsVisible
        { get { return onglet4IsVisible; } set { onglet4IsVisible = value; OnPropertyChanged(nameof(Onglet4IsVisible)); } }
        public bool Tous_les_adherents
        { get { return tous_les_adherents; } set { tous_les_adherents = value; OnPropertyChanged(nameof(Tous_les_adherents)); } }
        public bool Adherents_a_jour
        { get { return adherents_a_jour; } set { adherents_a_jour = value; OnPropertyChanged(nameof(Adherents_a_jour)); } }
        public bool Adherents_non_a_jour
        { get { return adherents_non_a_jour; } set { adherents_non_a_jour = value; OnPropertyChanged(nameof(Adherents_non_a_jour)); } }
        public bool ModeReel
        { get { return modeReel; } set { modeReel = value; OnPropertyChanged(nameof(ModeReel)); } }
        public bool ModeTest
        { get { return modeTest; } set { modeTest = value; OnPropertyChanged(nameof(ModeTest)); } }
        public bool Personalite
        { get { return personalite; } set { personalite = value; OnPropertyChanged(nameof(Personalite)); } }
        public bool Presse
        { get { return presse; } set { presse = value; OnPropertyChanged(nameof(Presse)); } }
        public bool Test
        { get { return test; } set { test = value; OnPropertyChanged(nameof(Presse)); } }
        public bool Inscrit
        { get { return inscrit; } set { inscrit = value; OnPropertyChanged(nameof(Inscrit)); } }
        public bool N_inscrit
        { get { return n_inscrit; } set { n_inscrit = value; OnPropertyChanged(nameof(N_inscrit)); } }
        public bool Signatured
        { get { return signatured; }
            set {
                signatured = value; 
               
                OnPropertyChanged(nameof(Signatured)); } 
        }
        public string Recherche
        { get { return recherche; } set { recherche = value; OnPropertyChanged(nameof(Recherche)); } }
        public bool Fiche_Selectionnes 
        { get { return fiche_Selectionnes; } set { fiche_Selectionnes = value;  OnPropertyChanged(nameof(Fiche_Selectionnes)); } }
        public bool NewMail 
        { get { return newMail; } set { newMail = value; OnPropertyChanged(nameof(NewMail)); } }
        public string IsEnvoye 
        { get { return isEnvoye; } set { isEnvoye = value; OnPropertyChanged(nameof(IsEnvoye)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
