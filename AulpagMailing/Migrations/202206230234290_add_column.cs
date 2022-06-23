namespace AulpagMailing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.mailings", "id_theme", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("public.mailings", "id_theme");
        }
    }
}
