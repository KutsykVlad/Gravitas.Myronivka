namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSingleWindowIsPreRegistered : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "IsPreRegistered", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "IsPreRegistered");
        }
    }
}
