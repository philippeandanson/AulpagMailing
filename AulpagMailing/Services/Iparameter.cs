using AulpagMailing.Models;
using AulpagMailing.ViewModels;
using System.Collections.ObjectModel;

namespace AulpagMailing.Services
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
