namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreRegisterCompanyUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreRegisterCompany", "Name", c => c.String());
            AddColumn("dbo.PreRegisterCompany", "EnterpriseCode", c => c.String());
            AddColumn("dbo.PreRegisterCompany", "ContactPhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreRegisterCompany", "ContactPhoneNumber");
            DropColumn("dbo.PreRegisterCompany", "EnterpriseCode");
            DropColumn("dbo.PreRegisterCompany", "Name");
        }
    }
}
