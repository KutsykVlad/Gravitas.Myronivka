namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreRegisterCompanyId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QueueRegister", "PreRegisterCompanyId", c => c.Long());
            CreateIndex("dbo.QueueRegister", "PreRegisterCompanyId");
            AddForeignKey("dbo.QueueRegister", "PreRegisterCompanyId", "dbo.PreRegisterCompany", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QueueRegister", "PreRegisterCompanyId", "dbo.PreRegisterCompany");
            DropIndex("dbo.QueueRegister", new[] { "PreRegisterCompanyId" });
            DropColumn("dbo.QueueRegister", "PreRegisterCompanyId");
        }
    }
}
