using AulpagMailing.Models;
using AulpagMailing.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AulpagMailing.Interfaces
{
    public interface Iparameter
    {
        void SetParameter(mailings parameter);

    }
    public interface Iparameter2
    {
        void SetParameter(destinataires parameter);

    }

}
