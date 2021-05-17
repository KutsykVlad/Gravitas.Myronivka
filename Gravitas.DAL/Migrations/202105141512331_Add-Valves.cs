namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValves : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.UnloadPointOpData", "Valve", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.UnloadPointOpData", "Valve");
        }
    }
}
