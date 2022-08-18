using System.ComponentModel;

namespace AulpagMailing.Models
{
    public class Boutons : INotifyPropertyChanged
    {
        private bool usager;
        private bool personalite;
        private bool presse;
        private bool test;
        private bool inscrit = true;
        private bool n_inscrit = true;
        private bool fiche_Selectionee;
        private string recherche;
        private string onglet3IsVisible;
        private string onglet4IsVisible;
        private string isEnvoye;
      
        public string Onglet3IsVisible
        { get { return onglet3IsVisible; } set { onglet3IsVisible = value; OnPropertyChanged(nameof(Onglet3IsVisible)); } }
        public string Onglet4IsVisible
        { get { return onglet4IsVisible; } set { onglet4IsVisible = value; OnPropertyChanged(nameof(Onglet4IsVisible)); } }
        public bool Usager
        { get { return usager; } set { usager = value; OnPropertyChanged(nameof(Usager)); } }
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
        public string Recherche
        { get { return recherche; } set { recherche = value; OnPropertyChanged(nameof(Recherche)); } }
        public bool Fiche_Selectionnes
        {get { return fiche_Selectionee; } set {fiche_Selectionee = value;  OnPropertyChanged(nameof(Fiche_Selectionnes)); } }
        public string IsEnvoye { get { return isEnvoye; } set { isEnvoye = value; OnPropertyChanged(nameof(IsEnvoye)); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
