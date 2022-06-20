namespace AulpagMailing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.destinataires",
                c => new
                    {
                        id_destinataire = c.Int(nullable: false, identity: true),
                        nom = c.String(),
                        prenom = c.String(),
                        civilité = c.String(),
                        email = c.String(),
                        categorie = c.Int(nullable: false),
                        titre = c.String(),
                        adherent = c.Boolean(nullable: false),
                        tutoiement = c.Boolean(),
                        adresse = c.String(),
                        ville = c.String(),
                        cp = c.String(),
                        debut = c.DateTime(),
                        fin = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_destinataire);
            
            CreateTable(
                "public.envoi",
                c => new
                    {
                        id_destinataire = c.Int(nullable: false),
                        fk_mailing = c.Int(nullable: false),
                        email = c.String(),
                        date_envoi = c.DateTime(nullable: false),
                        date_reception = c.DateTime(nullable: false),
                        lu = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_destinataire, t.fk_mailing })
                .ForeignKey("public.mailings", t => t.fk_mailing, cascadeDelete: true)
                .Index(t => t.fk_mailing);
            
            CreateTable(
                "public.mailings",
                c => new
                    {
                        id_mailing = c.Int(nullable: false, identity: true),
                        objet_mailing = c.String(),
                        contenu = c.String(),
                        date_creation = c.DateTime(nullable: false),
                        date_envoi = c.DateTime(nullable: false),
                        type_mailing = c.Int(nullable: false),
                        signature = c.String(),
                    })
                .PrimaryKey(t => t.id_mailing);
            
            CreateTable(
                "public.pjs",
                c => new
                    {
                        fk_mailing = c.Int(nullable: false),
                        piece = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.fk_mailing, t.piece });
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.envoi", "fk_mailing", "public.mailings");
            DropIndex("public.envoi", new[] { "fk_mailing" });
            DropTable("public.pjs");
            DropTable("public.mailings");
            DropTable("public.envoi");
            DropTable("public.destinataires");
        }
    }
}
