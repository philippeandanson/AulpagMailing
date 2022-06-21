namespace AulpagMailing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class theme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.theme",
                c => new
                    {
                        idtheme = c.Int(nullable: false, identity: true),
                        lbltheme = c.String(),
                    })
                .PrimaryKey(t => t.idtheme);
            
        }
        
        public override void Down()
        {
            DropTable("public.theme");
        }
    }
}
