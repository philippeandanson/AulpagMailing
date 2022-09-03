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
    public partial class destinataires : INotifyPropertyChanged
    {
        private bool   _selected;
        private string _prenom;
        private string _nom;
        private string _civilité;
        private int    _categorie;    
        private string _mail;
        private string _titre;
        private string _numadherent;

        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idcontact { get; set; }
        [Key, Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        [NotMapped]
        public bool selected
        { get { return _selected; } set { _selected = value; OnPropertyChanged(nameof(selected)); } }
        public string phone { get; set; }
        public string mobile { get; set; }
        public bool? tutoiement { get; set; } = false;
        public string adresse { get; set; }
        public string ville { get; set; }
        public string cp { get; set; }
        public DateTime? debut { get; set; }
        public DateTime? fin { get; set; }
        public DateTime? collecte { get; set; }
        public string numadherent
        { get { return _numadherent; } set { _numadherent = value; OnPropertyChanged(nameof(numadherent)); } }

        public destinataires() { }
        public destinataires(string _nom,string _prenom,string _civilite,string _email,int _categorie,string _titre,bool _adherent)
        {
            nom = _nom;
            prenom = _prenom;
            civilité = _civilite;
            email = _email;
            categorie = _categorie;
            titre = _titre;
         
        }
     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }

    public partial class destinataires
    {
        [NotMapped]
        public DateTime? dated { get; set; }
        public bool cotis_a_jour
        {
            get
            {
                if (dated > DateTime.Now)
                    return true;
                else
                    return false;
            }
        }
    }

    [Table("adhesion")]
    public class Adhesion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idAdhesion { get; set; }
        public int idMembre   { get; set; }
        public string categorie  { get; set; }
        public decimal montant    { get; set; }
        public DateTime dated { get; set; }
        public DateTime datef { get; set; }

    }

    [Table("adresse")]
    public class Adresse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idAdresse  { get; set; }
        public int idMembre   { get; set; }
        public string adresse { get; set; }
        public string ville   { get; set; }
        public string cp      { get; set; }

    }
    [Table("telephone")]
    public class Telephone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPhone       { get; set; }
        public int idMembre          { get; set; }
        public string type           { get; set; }
        public string phone          { get; set; }
        public DateTime date_constat { get; set; }
    }
    public class journaux
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idPresse { get; set; }
        public int idMembre { get; set; }
        public string journal { get; set; }
        public string email { get; set; }
    }
    public class elus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idElu { get; set; }
        public int idMembre { get; set; }
        public string fonction { get; set; }
        public string territoire { get; set; }
    }

    [Table("Destinataires_export")]
    [DelimitedRecord(";")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class Destinataires_export
    {
        public int id_destinataire { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string civilité { get; set; }
        public string email { get; set; }
        public int categorie { get; set; }
        public string titre { get; set; }

        public Destinataires_export() { }

        public Destinataires_export(int _id_destinataire,string _nom, string _prenom, string _civilite, string _email, int _categorie, string _titre)
        {
            id_destinataire = _id_destinataire;
            nom = _nom;
            prenom = _prenom;
            civilité = _civilite;
            email = _email;
            categorie = _categorie;
            titre = _titre;
        }
    }

    public class destinataire2 
    {
        [Key, Column(Order = 1)]
        public int idcontact { get; set; }
        [Key, Column(Order = 2)]
        public int id_destinataire { get; set; }
        public string nom { get; set; }     
        public string prenom { get; set; }   
        public string civilité { get; set; }      
        public string email { get; set; }
        public int categorie { get; set; }   
        public string titre { get; set; }  
        public string phone { get; set; }
        public string mobile { get; set; }
        public bool? tutoiement { get; set; } = false;
        public string adresse { get; set; }
        public string ville { get; set; }
        public string cp { get; set; }
        public DateTime? debut { get; set; }
        public DateTime? fin { get; set; }
        public DateTime? collecte { get; set; }
        public string numadherent { get; set; }
        public DateTime? dated { get; set; }      
 

    }
}
