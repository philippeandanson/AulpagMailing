using System.ComponentModel;

namespace AulpagMailing.ViewModels
{


    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool sauvegarde;
        public bool Sauvegarde
        { get => sauvegarde; set { sauvegarde = value; OnPropertyChanged(nameof(Sauvegarde)); } }
    
        private bool isPjActivable;
        public bool IsPjActivable
        { get => isPjActivable; set { isPjActivable = value; OnPropertyChanged(nameof(IsPjActivable)); } }

       

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
          
        }
    }
}
