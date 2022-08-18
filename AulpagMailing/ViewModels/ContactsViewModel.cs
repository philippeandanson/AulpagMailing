using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using AulpagMailing.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Windows.Data;

namespace AulpagMailing.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<destinataires> ListDestinataires { get; set; }
        public RelayCommand ModifFicheDestinataire { get; }
        public RelayCommand NewDestinataireCommand { get; }
        public CollectionView FiltreView           { get; set; }
        private Boutons bt;
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

        public ContactsViewModel()
        {
            bt = new Boutons();
            bt.Fiche_Selectionnes = true;
            bt.Recherche = "";
            ListDestinataires = Database.GetDestinataire();
            FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
            FiltreView.Filter = Contains;

            ModifFicheDestinataire = new RelayCommand(x =>
            {
                new FicheDestinataire(x as destinataires).ShowDialog();
                ListDestinataires = Database.GetDestinataire();
                FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
                FiltreView.Filter = Contains;
                OnPropertyChanged(nameof(ListDestinataires));
            });
            NewDestinataireCommand = new RelayCommand(x =>
            {
                new FicheDestinataire(new destinataires()).ShowDialog();
                ListDestinataires = Database.GetDestinataire();
                FiltreView = (CollectionView)CollectionViewSource.GetDefaultView(ListDestinataires);
                FiltreView.Filter = Contains;
                OnPropertyChanged(nameof(ListDestinataires));
            });


        }

        private bool Contains(object de)
        {

            destinataires destinataire = de as destinataires;
            bool t1 = destinataire.categorie == 1 && bt.Usager;
            bool t2 = destinataire.categorie == 2 && bt.Personalite;
            bool t3 = destinataire.categorie == 3 && bt.Presse;

            bool t6 = destinataire.nom.StartsWith(bt.Recherche);
            bool t7 = destinataire.selected && bt.Fiche_Selectionnes;
            bool t8 = bt.Fiche_Selectionnes;

            return ((t7 || ((t1 || t2 || t3) && t6)) && !t8) || t7 || (t6 && !string.IsNullOrEmpty(bt.Recherche));
        }   // Filtre les destinataires

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
