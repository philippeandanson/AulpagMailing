using FileHelpers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulpagMailing.Models
{
    [Table("destinataires")]
    [DelimitedRecord(";")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class destinataires : INotifyPropertyChanged
    {
        private bool   _selected;
        private string _prenom;
        private string _nom;
        private string _civilité;
        private int    _categorie;    
        private string _mail;
        private string _titre;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_destinataire { get; set; }       
        public string nom
        { get { return _nom; } set { _nom = value; OnPropertyChanged(nameof(nom)); } }
        public string prenom
        { get { return _prenom; } set { _prenom = value; OnPropertyChanged(nameof(prenom)); } }
        public string civilité
        { get { return _civilité; } set { _civilité = value; OnPropertyChanged(nameof(civilité)); } }
        public string email
        { get { return _mail; } set { _mail = value; OnPropertyChanged(nameof(email)); } }
        public int categorie
        { get { return _categorie; } set { _categorie = value; OnPropertyChanged(nameof(categorie)); } }
        public string titre       
        { get { return _titre; }set { _titre = value; OnPropertyChanged(nameof(titre));} }
        public bool adherent       { get; set; }
        [NotMapped]
        public bool selected
        { get { return _selected; } set { _selected = value; OnPropertyChanged(nameof(selected)); } }
        public bool? tutoiement { get; set; } = false;
        public string adresse { get; set; }
        public string ville { get; set; }
        public string cp { get; set; }
        public DateTime? debut { get; set; }
        public DateTime? fin { get; set; }

        public destinataires() { }
        public destinataires(string _nom,string _prenom,string _civilite,string _email,int _categorie,string _titre,bool _adherent)
        {
            nom = _nom;
            prenom = _prenom;
            civilité = _civilite;
            email = _email;
            categorie = _categorie;
            titre = _titre;
            adherent = _adherent;
        }
     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }

    [Table("Destinataires_export")]
    [DelimitedRecord(";")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class Destinataires_export
    {
        public string nom { get; set; }
        public string prenom { get; set; }
        public string civilité { get; set; }
        public string email { get; set; }
        public int categorie { get; set; }    
        public bool adherent { get; set; }
        public string titre { get; set; }

        public Destinataires_export() { }

        public Destinataires_export(string _nom, string _prenom, string _civilite, string _email, int _categorie, bool _adherent, string _titre)
        {
            nom = _nom;
            prenom = _prenom;
            civilité = _civilite;
            email = _email;
            categorie = _categorie;          
            adherent = _adherent;
            titre = _titre;
        }
    }

    public class Complement_presse
    {
        [Key]
        public int id_destinataire { get; set; }
        public string journal { get; set; }
        public string email { get; set; }
    }
    public class Complement_personalite
    {
        [Key]
        public int id_destinataire { get; set; }
        public string fonction { get; set; }
        public string territoire { get; set; }
    }
}
