using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulpagMailing.Models
{
    public class parametres : INotifyPropertyChanged
    {
        private string _parametre;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_parametre { get; set; }
        public string parametre
        { get { return _parametre; } set { _parametre = value; OnPropertyChanged("parametre") ;} }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }


}
