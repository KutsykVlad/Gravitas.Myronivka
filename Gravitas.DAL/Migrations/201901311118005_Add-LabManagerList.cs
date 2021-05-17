namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLabManagerList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.LabManagerList",
                    c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabManagerList", "EmployeeId", "ext.Employee");
            DropIndex("dbo.LabManagerList", new[] { "EmployeeId" });
            DropTable("dbo.LabManagerList");
        }
    }
}
