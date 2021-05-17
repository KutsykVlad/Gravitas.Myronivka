namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPreRegisterCompanyTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreRegisterCompany",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(),
                        AllowToAdd = c.Boolean(nullable: false),
                        TrucksMax = c.Int(nullable: false),
                        TrucksInProgress = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PreRegisterCompany");
        }
    }
}
