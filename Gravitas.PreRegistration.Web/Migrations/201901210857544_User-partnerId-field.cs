namespace Gravitas.PreRegistration.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserpartnerIdfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PartnerId", c => c.String());
            AddColumn("dbo.AspNetUsers", "IsRegistrationAllowed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsRegistrationAllowed");
            DropColumn("dbo.AspNetUsers", "PartnerId");
        }
    }
}
