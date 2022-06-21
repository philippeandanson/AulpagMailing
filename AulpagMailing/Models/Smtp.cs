using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulpagMailing.Models
{
    [Table("smtp")]
    public class Smtp : INotifyPropertyChanged
    {
        private string _host;
        private int _port;
        private string _compte;
        private string _mdp;
        private bool? _actif;
        
        
        [Key]
        public string host   { get { return _host; } set { _host = value; OnPropertyChanged(nameof(host));}}
        public int port      { get { return _port; } set { _port = value; OnPropertyChanged(nameof(port)); }}    
        public string  compte  { get { return _compte; } set { _compte = value; OnPropertyChanged(nameof(compte)); }}
        public string mdp { get { return _mdp; } set { _mdp = value; OnPropertyChanged(nameof(mdp)); } }
        public bool? actif { get { return _actif; } set { _actif = value; OnPropertyChanged(nameof(actif)); } }
     

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        }
    }
}
