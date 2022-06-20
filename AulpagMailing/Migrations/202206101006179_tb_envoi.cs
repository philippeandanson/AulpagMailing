namespace AulpagMailing.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tb_envoi : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.envoi", "contenu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.envoi", "contenu");
        }
    }
}
