using System.Collections.Generic;
using System.Collections.ObjectModel;
using AulpagMailing.Models;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.IO;

namespace AulpagMailing.Data
{
    public class Database
    {
         static string connetionString = "provider = PCSoft.HFSQL; initial catalog = parisgra-02; data source = paris-granville.org;User ID=parisgra; password=0ftH1EkU5bH52QFh3B "; //+  "extended properties = 'Language=ISO-8859-1'";        
         public static OleDbConnection cnn = new OleDbConnection(connetionString);
 
        private static readonly object locker = new object();

        public class test
        {
            
            public int id_mailing { get; set; }
            public string objet_mailing { get; set; }
            public string contenu { get; set; }
            public DateTime date_creation { get; set; }
            public DateTime date_envoi { get; set; }
            public int type_mailing { get; set; }
            public string signature { get; set; }
            public int? id_theme { get; set; }         
            public int envoye { get; set; }      
            public int reste { get; set; }          
            public int total { get; set; }         
            public List<Envoi> Items { get; set; }
        }

        public static ObservableCollection<mailings> ReadMailing
        {
            get
            {

                string SQL = "	WITH sqltotal AS (	" +
"	         SELECT envoi.fk_mailing,	" +
"	            count(*) AS total	" +
"	           FROM envoi	" +
"	          GROUP BY envoi.fk_mailing	" +
"	        ), sql1 AS (	" +
"	         SELECT envoi.fk_mailing,	" +
"	            count(*) AS envoye	" +
"	           FROM envoi	" +
"	          WHERE envoi.date_envoi <> '-infinity'::timestamp without time zone	" +
"	          GROUP BY envoi.fk_mailing	" +
"	        ), sql2 AS (	" +
"	         SELECT envoi.fk_mailing,	" +
"	            count(*) AS reste	" +
"	           FROM envoi	" +
"	          WHERE envoi.date_envoi = '-infinity'::timestamp without time zone	" +
"	          GROUP BY envoi.fk_mailing	" +
"	        ), sql3 AS (	" +
"	         SELECT a.fk_mailing,	" +
"	                CASE	" +
"	                    WHEN b.envoye IS NULL THEN 0::bigint	" +
"	                    ELSE b.envoye	" +
"	                END AS envoye,	" +
"	                CASE	" +
"	                    WHEN c.reste IS NULL THEN 0::bigint	" +
"	                    ELSE c.reste	" +
"	                END AS reste,	" +
"	            a.total	" +
"	           FROM sqltotal a	" +
"	             LEFT JOIN sql1 b ON a.fk_mailing = b.fk_mailing	" +
"	             LEFT JOIN sql2 c ON a.fk_mailing = c.fk_mailing	" +
"	        ) " +
"	         SELECT a.id_mailing,	" +
"	            a.objet_mailing,	" +
"	            a.contenu,	" +
"	            a.date_creation,	" +
"	            a.date_envoi,	" +
"	            a.type_mailing,	" +
"	            a.signature,	" +
"	            a.id_theme,	" +
"	            b.envoye,	" +
"	            b.reste,	" +
"	            b.total	" +
"	           FROM mailings a	" +
"	             LEFT JOIN sql3 b ON a.id_mailing = b.fk_mailing Order by a.id_mailing	";

                lock (locker)
                {
                    ObservableCollection<mailings> obj = new ObservableCollection<mailings>();
                    using (BaseContext context = new BaseContext())
                    {
                        List<liste_mailing> mailingsQuery = context.Database.SqlQuery<liste_mailing>(SQL).ToList<liste_mailing>();
                        // List <mailings> mailingsQuery = context.Database.SqlQuery<mailings>(SQL).ToList<mailings>();                    
                        foreach (var t in mailingsQuery)
                        {
                            
                            obj.Add(new mailings()
                            {
                                id_mailing=t.id_mailing ,
                                contenu=t.contenu,
                                date_creation=t.date_creation,
                                date_envoi=t.date_envoi,
                                envoye=t.envoye,
                                id_theme=t.id_theme,
                                objet_mailing=t.objet_mailing,
                                reste=t.reste,
                                signature=t.signature,
                                total=t.total,
                                type_mailing=t.type_mailing
                            });
                        }

                        return obj;
                    }
                }

            }
        }

        public static mailings GetLastMailing
        {
            get
            {

                lock (locker)
                {
                    mailings ml = new mailings();
                   
                    using (BaseContext context = new BaseContext())
                    {

                        int t = 0;
                        try
                        {
                            t = context.mailings.Where(p => !string.IsNullOrEmpty(p.signature)).Select(x => x.id_mailing).Max();
                            ml = context.mailings.Where(x => x.id_mailing == t).First();
                        }
                        catch { }

                        return ml;
                        
                    }
                }

            }
        }

        public static pjs GetLastPjs
        {
            get
            {

                lock (locker)
                {
                    pjs piece = new pjs();

                    using (BaseContext context = new BaseContext())
                    {

                        int t = 0;
                        try
                        {
                            t = context.pieces.Select(x => x.fk_mailing).Max();
                            piece = context.pieces.Where(x => x.fk_mailing == t).First();
                        }
                        catch { }

                        return piece;

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

        public static Smtp GetSmtpActif
        {
            get
            {

                lock (locker)
                {
                    ObservableCollection<Smtp> obj = new ObservableCollection<Smtp>();
                    using (BaseContext context = new BaseContext())
                    {
                        List<Smtp> mailingsQuery = (from item in context.smtps orderby item.actif descending select item).ToList();
                        Smtp smtp = mailingsQuery.Where(x => x.actif == true).First();

                        return smtp;
                    }
                }

            }
        }

        public static void DeleteSmtp(Smtp item)
        {

            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        var entity = db.smtps.First(a => a.idsmtp == item.idsmtp);

                        db.smtps.Remove(entity);

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
                    { 
                        db.SaveChanges();
                    }
                    catch (Exception Ex) 
                    { 
                    }
                }
            }
        }

        public static List<Destinataires_export> GetDestinataires
        {
            get
            {

                string SQL = "SELECT  nom, prenom, civilité, email, categorie, titre  " +
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

        public static destinataires GetFirstContact()
        {

            lock (locker)
            {
                using (BaseContext context = new BaseContext())
                {
                    int result = context.destinataires.Count();
                    if (result == 0) return null;
                    return context.destinataires.First();

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
                    foreach (var item in pieces)
                    {
                        item.affiche_piece= Path.GetFileName(item.piece);
                        obj.Add(item);
                    }
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

        public static void DeleteCurrentEnvoi(Envoi item)
        {

            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        var entity = db.envois.First(a => a.fk_mailing == item.fk_mailing);
                        db.envois.Remove(entity);

                        db.SaveChanges();

                    }

                    catch (Exception Ex) { }
                }

            }
        }

        public static void  DeleteLastPreparationEnvoi(int key)
        {
            string SQL = "delete  from envoi  where  date_envoi='-infinity' and  fk_mailing=" + key ;

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

        public static void  DeleteAllContacts()
        {
            lock (locker)
            {
                using (BaseContext db = new BaseContext())
                {
                    try
                    {
                        db.destinataires.RemoveRange(db.destinataires);
                        db.SaveChanges();
                    }

                    catch (Exception Ex)
                    {
                    }
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

        public static void  UpdateEnvoi(Envoi item)
        {
            lock (locker)
            {
                using (var db = new BaseContext())
                {
                    db.envois.AddOrUpdate(item);
                    try
                    { db.SaveChanges(); }
                    catch (Exception Ex) { }
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
                item_base.id_destinataire = item_csv.id_destinataire;
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

        public static int   UpdateMailing(mailings item)
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

        public static void  UpdateParametres(parametres item)
        {
            lock (locker)
            {
            
                using (var db = new BaseContext())
                {
                    db.parameters.AddOrUpdate(item);
                    db.SaveChanges();
                   
                }
            }
        }
   
        public static List<parametres> GetParametres
        {
            get
            {

                lock (locker)
                {
                   
                    using (BaseContext context = new BaseContext())
                    {
                       return (from item in context.parameters  select item).ToList();
                    }
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
        // Liste des adhérents
        public static void UpdateAdherent()
        {
           // cnn.Open();
            string RSql = "Select *  from Membre ";
            OleDbCommand cmd = new OleDbCommand(RSql, cnn);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (reader.GetString(3) != "" && reader.GetBoolean(46) == false)
                {

                    destinataires contact = new destinataires()
                    {
                        id_destinataire = reader.GetInt32(0),
                        civilité = reader.GetString(16),
                        nom = reader.GetString(18),
                        prenom = reader.GetString(17),
                        email = reader.GetString(3),
                        adresse = reader.GetString(6),
                        ville = reader.GetString(9),
                        cp = reader.GetString(8),
                        debut = reader.GetDateTime(12),
                        numadherent = reader.GetString(11),
                        categorie = 1,
                        collecte=DateTime.Now
                    };

                    List<destinataires> t = GetDestinataires_All.Where(x => x.id_destinataire == contact.id_destinataire).ToList();                 
                    if (t.Count() != 0) contact.idcontact = t.FirstOrDefault(x => x.id_destinataire == contact.id_destinataire).idcontact;
                   
                    UpdateDestinataire(contact);
                }
              
            }
         //   cnn.Close();
        }

        public static void UpdateAdhesion()
        {

           
            string RSql = "Select *  from membre_adhesion ";
            OleDbCommand cmd = new OleDbCommand(RSql, cnn);
            OleDbDataReader reader = cmd.ExecuteReader();

            using (var db = new BaseContext())
            {

                while (reader.Read())
                {
                   
                    Adhesion contact = new Adhesion()
                    {
                        idAdhesion  = reader.GetInt32(3),
                        idMembre    = reader.GetInt32(0),
                        categorie   = reader.GetString(1),
                        montant     = reader.GetDecimal(2),
                        dated       = reader.GetDateTime(5),
                        datef       = reader.GetDateTime(6)
                    };


                    db.adhesions.AddOrUpdate(contact);
                    try
                    {db.SaveChanges();}
                    catch (Exception Ex)  {}

                }

            }


          

        }

       public static ObservableCollection<destinataires> GetAdherents()
        {

         string SQL = "	WITH sql1 AS (	" + 
        "SELECT adhesion.\"idMembre\",	" +
        " max(adhesion.dated) AS dated	" +
        " FROM adhesion	" +
        " GROUP BY adhesion.\"idMembre\"	" +
        " ORDER BY adhesion.\"idMembre\"	" +
        " ), sql2 AS (	" +
        " SELECT a_1.\"idMembre\" ,	" +
        " a_1.categorie as c_tarif,	" +
        " a_1.montant,	" +
        " a_1.dated,	" +
        " a_1.datef	" +
        " FROM adhesion a_1	" +
        " JOIN sql1 b_1 ON a_1.\"idMembre\" = b_1.\"idMembre\" AND a_1.dated = b_1.dated	" +
        "  ORDER BY a_1.\"idMembre\"	" +
        " )	" +
        "	 SELECT Distinct " +
        "       a.id_destinataire,	" +
        "       a.idcontact,	" +
        "       a.civilité,	" +
        "       a.titre,	" +
        "       a.nom,	" +
        "	    a.prenom,  	" +
        "	    a.email,	" +
        "	    a.phone ,	" +
        "	    a.mobile ,	" +
        "	    a.mobile ,	" +
        "       a.tutoiement ,"  +
        "       a.adresse ," +
        "       a.ville ," +
        "       a.cp ," +
        "       a.debut ," +
        "       a.fin ," +
        "       a.collecte , " +
        "	    a.numadherent,	" +
        "	    a.categorie,	" +
        "	    b.dated + INTERVAL '365 day' as dated	" +
        "	   FROM destinataires a	" +
        "	     LEFT JOIN sql2 b ON a.id_destinataire = b.\"idMembre\"	" +
        "	     where dated is not null	" +
        "	    ORDER BY a.nom;	";

            ObservableCollection<destinataires> obj = new ObservableCollection<destinataires>();
            using (BaseContext context = new BaseContext())
            {
                List<destinataire2> destinatairesQuery2 = context.Database.SqlQuery<destinataire2>(SQL).ToList<destinataire2>();
                List<destinataires> destinatairesQuery = context.Database.SqlQuery<destinataires>(SQL).ToList<destinataires>();

                foreach (var item in destinatairesQuery)
                {
                    var temp = destinatairesQuery2.First(x => x.id_destinataire == item.id_destinataire);
                    item.dated = temp.dated;                    
                    obj.Add(item);
                }
                return obj;               
            }

        }
    }
}
