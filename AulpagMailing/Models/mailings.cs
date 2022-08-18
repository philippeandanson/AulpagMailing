using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulpagMailing.Models
{
    public class mailings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      
       public int             id_mailing     { get; set; }
       public string          objet_mailing  { get; set; }
       public string          contenu        { get; set; }     
       public DateTime        date_creation  { get; set; }
       public DateTime        date_envoi     { get; set; }
       public int             type_mailing   { get; set; }
       public string          signature      { get; set; }
       public int?            id_theme       { get; set; }
       [NotMapped]
       public int?            envoye          { get; set; }
       [NotMapped]
       public int?           reste           { get; set; }
       [NotMapped]
       public int?           total           { get; set; }

        [ForeignKey("fk_mailing")]
        public List<Envoi> Items { get; set; }
    }


    public class liste_mailing
    {
      
        public int id_mailing { get; set; }
        public string objet_mailing { get; set; }
        public string contenu { get; set; }
        public DateTime date_creation { get; set; }
        public DateTime date_envoi { get; set; }
        public int type_mailing { get; set; }
        public string signature { get; set; }
        public int? id_theme { get; set; }      
        public int? envoye { get; set; }       
        public int? reste { get; set; }    
        public int? total { get; set; }     
      
    }

    public class pjs : INotifyPropertyChanged
    {
        private int _fk_mailing;
        private string _piece;

        [Key, Column(Order = 1)]
        public int fk_mailing
        { get { return _fk_mailing; }   set { _fk_mailing = value; OnPropertyChanged(nameof(fk_mailing));} }
        [Key, Column(Order = 2)]
       
        
        public string   piece
       { get { return _piece; } set { _piece = value; OnPropertyChanged(nameof(piece)); } }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    [Table ("envoi")]
    public class Envoi
    {
        [Key, Column(Order = 1)]
        public int id_destinataire      { get; set; }
        [Key, Column(Order = 2)]
        public int fk_mailing           { get; set; }
        public string email             { get; set; }
        public string contenu           { get; set; }
        public DateTime date_envoi      { get; set; }
        public DateTime date_reception  { get; set; }
        public bool lu                  { get; set; }
        [NotMapped]
        public string logo               { get; set; }

    }

}
