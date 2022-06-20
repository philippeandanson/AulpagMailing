using System.Collections.Generic;
using System.Collections.ObjectModel;
using AulpagMailing.Models;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AulpagMailing.Data
{
    public class Database
    {
        static string connetionString = "provider = PCSoft.HFSQL; initial catalog = parisgra-02; data source = paris-granville.org;User ID=parisgra; password=0ftH1EkU5bH52QFh3B "; //+  "extended properties = 'Language=ISO-8859-1'";        
        static OleDbConnection cnn = new OleDbConnection(connetionString);

        private static readonly object locker = new object();

        public static ObservableCollection<mailings> ReadMailing
        {
            get
            {

                lock (locker)
                {
                    ObservableCollection<mailings> obj = new ObservableCollection<mailings>();
                    using (BaseContext context = new BaseContext())
                    {
                        List<mailings> mailingsQuery = (from item in context.mailings orderby item.id_mailing select item).ToList();
                        foreach (var t in mailingsQuery) obj.Add(t);

                        return obj;
                    }
                }

            }
        }

        public static ObservableCollection<Smtp> GetSmtp
        {
            get
            {

                lock (locker)
                {
                    ObservableCollection<Smtp> obj = new ObservableCollection<Smtp>();
                    using (BaseContext context = new BaseContext())
                    {
                        List<Smtp> mailingsQuery = (from item in context.smtps orderby item.actif descending  select item).ToList();
                        foreach (var t in mailingsQuery) obj.Add(t);

                        return obj;
                    }
                }

            }
        }

        public static List<Destinataires_export> GetDestinataires
        {
            get
            {

                string SQL = "SELECT  nom, prenom, civilité, email, categorie, adherent, titre  " +
                    "FROM public.destinataires order by categorie,nom";
                lock (locker)
                {
                    using (BaseContext context = new BaseContext())
                    {
                        List<Destinataires_export> dest = context.Database.SqlQuery<Destinataires_export>(SQL).ToList<Destinataires_export>();
                        //IQueryable<destinataires> destinataires = from item in context.destinataires orderby item.id_origine select item;
                        return dest;
                    }
                }
            }
        }

        public static ObservableCollection<Envoi> GetEnvoiMail(int idMailing)
        {
            // Obtenir la liste des e-mail enregistré pour l'envoi
            ObservableCollection<Envoi> obj = new ObservableCollection<Envoi>();
            using (BaseContext context = new BaseContext())
            {
                List<Envoi> envoiQuery = (from item in context.envois select item).Where(x => x.fk_mailing == idMailing).ToList();
                foreach (var item in envoiQuery) obj.Add(item);
                return obj;
            }
        }

        public static ObservableCollection<destinataires> GetDestinataire()
        {
            ObservableCollection<destinataires> obj = new ObservableCollection<destinataires>();
            using (BaseContext context = new BaseContext())
            {
                List<destinataires> destinatairesQuery = (from item in context.destinataires orderby item.categorie, item.nom select item).ToList();
                foreach (var item in destinatairesQuery) obj.Add(item);
                return obj;
            }

        }

        public static List<destinataires> GetDestinataires_All
        {
            get
            {


                lock (locker)
                {
                    using (BaseContext context = new BaseContext())
                    {
                        // List<Destinataires_export> dest = context.Database.SqlQuery<Destinataires_export>(SQL).ToList<Destinataires_export>();
                        IQueryable<destinataires> destinataires = from item in context.destinataires select item;
                        return destinataires.ToList();
                    }
                }
            }
        }

        public static destinataires GetDestinataireById(int idDestinataire)
        {

            lock (locker)
            {
                using (BaseContext context = new BaseContext())
                {

                    return context.destinataires.First(x => x.id_destinataire == idDestinataire);

                }
            }

        }

        public static ObservableCollection<pjs> GetPiecesJointes(mailings mailing)
        {
            ObservableCollection<pjs> obj = new ObservableCollection<pjs>();
            lock (locker)
            {
                using (BaseContext context = new BaseContext())
                {
                    List<pjs> pieces = (from item in context.pieces select item).Where(x => x.fk_mailing == mailing.id_mailing).ToList();
                    foreach (var item in pieces) obj.Add(item);
                    return obj;
                }

            }
        }

        public static void  DeleteDossier(mailings item)
        {

            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        var entity = db.mailings.First(a => a.id_mailing == item.id_mailing);

                        db.mailings.Remove(entity);

                        db.SaveChanges();

                    }

                    catch (Exception Ex) 
                    {
                    
                    }
                }

            }
        }

        public static void  DeleteSmtp(Smtp item)
        {

            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        var entity = db.smtps.First(a => a.host == item.host);

                        db.smtps.Remove(entity);

                        db.SaveChanges();

                    }

                    catch (Exception Ex)
                    {

                    }
                }

            }
        }

        public static void  DeletePiecesJointes(pjs item)
        {

            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        var entity = db.pieces.First(a => a.fk_mailing == item.fk_mailing);
                        db.pieces.Remove(entity);

                        db.SaveChanges();

                    }

                    catch (Exception Ex) { }
                }

            }
        }

        public static void  DeleteLastPreparationEnvoi(int key)
        {
            string SQL = "delete  from envoi where fk_mailing=" + key ;

            using (BaseContext db = new BaseContext())
            {
                int count =  db.Database.ExecuteSqlCommand(SQL);
            }
          
        }

        public static void  DeleteDestinataire(destinataires item)
        {

            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        var entity = db.destinataires.First(a => a.id_destinataire== item.id_destinataire);
                        db.destinataires.Remove(entity);

                        db.SaveChanges();

                    }

                    catch (Exception Ex) { }
                }

            }
        }

        public static void  UpdatePjAsync(pjs item)
        {
            lock (locker)
            {              
                using (var db = new BaseContext())
                {               
                   db.pieces.AddOrUpdate(item);           
                    try
                    { db.SaveChanges();}
                    catch (Exception Ex){ }
                }
            }
        }
     
        public static void  ImportDestinataire(IEnumerable<Destinataires_export> FromCsv)
        {
            var destinataires = GetDestinataires_All;
            lock (locker)
            {
                // int? id = item.id_destinataire;
                using (var db = new BaseContext())
                {

               foreach (var item_csv in FromCsv)
                {
                var item_base = destinataires.FirstOrDefault(y => y.nom == item_csv.nom && y.prenom == item_csv.prenom);
                if (item_base == null) item_base = new destinataires();
                item_base.adherent = item_csv.adherent;
                item_base.categorie = item_csv.categorie;
                item_base.civilité = item_csv.civilité;
                item_base.nom = item_csv.nom;
                item_base.prenom = item_csv.prenom;
                item_base.titre = item_csv.titre;
                item_base.email = item_csv.email;
                  
                 try {db.destinataires.AddOrUpdate(item_base);  }
                    catch (Exception Ex) 
                        {                  }
                }

                    db.SaveChanges();
             }

         }
    }

        public static void  UpdateDestinataire(destinataires item)
        {
            lock (locker)
            {
                int? id = item.id_destinataire;
                using (var db = new BaseContext())
                {                                  
                    db.destinataires.AddOrUpdate(item);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception Ex)
                    {
                    }
                }
            }
        }

        public static void  UpdateEnvoiFromEmail(Envoi item)
        {
            lock (locker)
            {
               
                using (var db = new BaseContext())
                {
                    db.envois.AddOrUpdate(item);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception Ex)
                    {
                    }
                }
            }
        }

        public static void UpdateSmtp(Smtp item)
        {
            lock (locker)
            {
                using (var db = new BaseContext())
                {
                    db.smtps.AddOrUpdate(item);
                    try
                    { db.SaveChanges(); }
                    catch (Exception Ex) { }
                }
            }
        }

        public static int UpdateMailing(mailings item)
        {

            lock (locker)
            {
                int? id = item.id_mailing;
                using (var db = new BaseContext())
                {
                    db.mailings.AddOrUpdate(item);
                    db.SaveChanges();
                    return item.id_mailing; // Yes it's here
                }
            }
        }

        public static async  Task<bool> Adhérents()
        {
            string RSql = "Select * from Membre";

            Task<bool> result = Task.Run(() =>
          {
              destinataires dt = new destinataires();
              
              OleDbCommand cmd = new OleDbCommand(RSql, Utilitaires.Connexion);
              OleDbDataReader reader = cmd.ExecuteReader();

              try
              {
                 

                  while (reader.Read())
                  {
                      var db = new BaseContext();         
                      dt.email       = (string)reader.GetValue(3);
                      dt.categorie   = 1;
                      dt.civilité    = (string)reader.GetValue(16);
                      dt.prenom      = (string)reader.GetValue(17);
                      dt.nom         = (string)reader.GetValue(18);
                      dt.adherent    = true;                    
                      db.destinataires.Add(dt);
                      db.SaveChanges();

                  }

                

                
              }
              catch (Exception ex)
              {

              }
              return true;


          });

         return await result;
           
        }

        public static async  Task<bool> MembreArchive()
        {
            string RSql = "Select * from MEMBRE_ARCHIVE";

            Task<bool> result = Task.Run(() =>
            {
                destinataires dt = new destinataires();

                OleDbCommand cmd = new OleDbCommand(RSql, Utilitaires.Connexion);
                OleDbDataReader reader = cmd.ExecuteReader();

                try
                {


                    while (reader.Read())
                    {
                        var db = new BaseContext();                   
                        dt.email = (string)reader.GetValue(3);
                        dt.categorie = 1;
                        dt.civilité = (string)reader.GetValue(16);
                        dt.prenom = (string)reader.GetValue(17);
                        dt.nom = (string)reader.GetValue(18);
                        dt.adherent = true;
                        db.destinataires.Add(dt);
                        db.SaveChanges();

                    }




                }
                catch (Exception ex)
                {

                }
                return true;


            });

            return await result;

        }

        public static async  Task<bool> Personalites()
        {
            string RSql = "Select * from Destinataire";

            Task<bool> result = Task.Run(() =>
            {
                destinataires dt = new destinataires();

                OleDbCommand cmd = new OleDbCommand(RSql, Utilitaires.Connexion);
                OleDbDataReader reader = cmd.ExecuteReader();

                try
                {


                    while (reader.Read())
                    {
                        var db = new BaseContext();
                    
                        dt.email = (string)reader.GetValue(8);
                        dt.categorie = 2;
                        dt.civilité = (string)reader.GetValue(5);
                        dt.prenom = (string)reader.GetValue(4);
                        dt.nom = (string)reader.GetValue(3);
                        dt.titre= (string)reader.GetValue(2);
                        dt.adherent = false;
                        db.destinataires.Add(dt);
                        db.SaveChanges();

                    }




                }
                catch (Exception ex)
                {

                }
                return true;


            });

            return await result;

        }

    }
}
