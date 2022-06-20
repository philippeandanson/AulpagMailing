using System;
using System.Data.Entity.Migrations;

namespace AulpagMailing.Migrations
{
    public partial class smtp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.smtp",
                c => new
                    {
                        host = c.String(nullable: false, maxLength: 128),
                        port = c.Int(nullable: false),
                        compte = c.String(),
                        mdp = c.String(),
                    })
                .PrimaryKey(t => t.host);
            
        }
        
        public override void Down()
        {
            DropTable("public.smtp");
        }
    }
}
