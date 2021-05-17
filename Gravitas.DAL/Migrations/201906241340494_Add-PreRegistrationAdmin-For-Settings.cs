namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreRegistrationAdminForSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Settings", "PreRegistrationAdminEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Settings", "PreRegistrationAdminEmail");
        }
    }
}
