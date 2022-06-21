using AulpagMailing.Models;
using System.Data.Entity;
using System.Data.OleDb;

namespace AulpagMailing.Data
{
    class BaseContext : DbContext
    {
        public DbSet<mailings>      mailings      { get; set; }
        public DbSet<pjs>           pieces        { get; set; }
        public DbSet<destinataires> destinataires { get; set; }
        public DbSet<Envoi>         envois        { get; set; }
        public DbSet<Smtp>          smtps         { get; set; }
        public DbSet<Themes>        themes        { get; set; }


        public BaseContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }

    static class Utilitaires
     {
        private static OleDbConnection m_cnxBase = null;

        public static OleDbConnection Connexion
        {
            get
            {
                string connetionString = "provider = PCSoft.HFSQL; initial catalog = parisgra-02;" +
                " data source = paris-granville.org;User ID=parisgra; password=0ftH1EkU5bH52QFh3B "; //+  "extended properties = 'Language=ISO-8859-1'";   
                if (m_cnxBase == null)
                    m_cnxBase = new OleDbConnection(connetionString);
                if (m_cnxBase.State != System.Data.ConnectionState.Open)
                    m_cnxBase.Open();

                return m_cnxBase;
            }
        }
    }
}
