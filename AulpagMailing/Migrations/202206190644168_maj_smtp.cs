namespace AulpagMailing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maj_smtp : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.smtp", "actif", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("public.smtp", "actif");
        }
    }
}
