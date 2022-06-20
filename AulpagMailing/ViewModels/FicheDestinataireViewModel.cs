using AulpagMailing.Data;
using AulpagMailing.Models;
using AulpagMailing.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AulpagMailing.ViewModels
{
    public class FicheDestinataireViewModel : INotifyPropertyChanged, Iparameter2
    {

        private string _selectedCategorie;
        public string SelectedCategorie
        {
            get { return _selectedCategorie; }
            set
            {
                if (_selectedCategorie == value) return;

                _selectedCategorie = value;
                OnPropertyChanged("SelectedCategorie");
            }
        }
     
        public string[]      Categories { get; set; }
      
        public destinataires CurrentDestinataire { get; set; }
        public RelayCommand ClosedCommand   { get; }
        public RelayCommand DeleteCommand   { get; }
        public RelayCommand ValidateCommand { get; }

        private bool closeTrigger;
        public bool CloseTrigger
        {
            get { return this.closeTrigger; }
            set
            {
                this.closeTrigger = value;
                OnPropertyChanged(nameof(CloseTrigger));
            }
        }

        public FicheDestinataireViewModel()
        {
            Categories = new[] { "Usager", "Personalité", "Presse" };
           
            ClosedCommand = new RelayCommand(x =>
            {
                CloseTrigger = true;
            });
            DeleteCommand = new RelayCommand(x =>
            {
                Database.DeleteDestinataire(CurrentDestinataire);                           
                CloseTrigger = true;
            });
            ValidateCommand = new RelayCommand(x =>
            {
                switch (SelectedCategorie)
                {

                    case "Usager":
                        CurrentDestinataire.categorie=1;
                        break;
                    case "Personalité":
                        CurrentDestinataire.categorie = 2;
                        break;
                    case "Presse":
                        CurrentDestinataire.categorie = 3;
                        break;
                }

                Database.UpdateDestinataire(CurrentDestinataire);              
                CloseTrigger = true;
            });
        }


        public void SetParameter(destinataires parameter)
        {
            CurrentDestinataire = parameter as destinataires;
            switch  (CurrentDestinataire.categorie)
                {

                case 1:
                    SelectedCategorie = "Usager";
                    break;
                case 2:
                    SelectedCategorie = "Personalité";
                    break;
                case 3:
                    SelectedCategorie = "Presse";
                    break;
                }
            OnPropertyChanged("CurrentInfo");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
