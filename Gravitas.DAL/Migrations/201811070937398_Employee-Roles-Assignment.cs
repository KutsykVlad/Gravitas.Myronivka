namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeRolesAssignment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Card", "ExternalUserId", "ext.User");
            DropForeignKey("dbo.OpVisa", "ExternalUserId", "ext.User");
            DropIndex("dbo.OpVisa", new[] { "ExternalUserId" });
            DropIndex("dbo.Card", new[] { "ExternalUserId" });
            CreateTable(
                "dbo.RoleAssignment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RoleId = c.Long(nullable: false),
                        NodeId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Node", t => t.NodeId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.NodeId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeRoles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.String(nullable: false, maxLength: 250),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.RoleId);
            
            AddColumn("dbo.OpVisa", "EmployeeId", c => c.String(maxLength: 250));
            AddColumn("dbo.Card", "EmployeeId", c => c.String(maxLength: 250));
            CreateIndex("dbo.Card", "EmployeeId");
            CreateIndex("dbo.OpVisa", "EmployeeId");
            AddForeignKey("dbo.Card", "EmployeeId", "ext.Employee", "Id");
            AddForeignKey("dbo.OpVisa", "EmployeeId", "ext.Employee", "Id");
            DropColumn("dbo.OpVisa", "ExternalUserId");
            DropColumn("dbo.Card", "ExternalUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Card", "ExternalUserId", c => c.String(maxLength: 250));
            AddColumn("dbo.OpVisa", "ExternalUserId", c => c.String(maxLength: 250));
            DropForeignKey("dbo.RoleAssignment", "RoleId", "dbo.Role");
            DropForeignKey("dbo.EmployeeRoles", "RoleId", "dbo.Role");
            DropForeignKey("dbo.EmployeeRoles", "EmployeeId", "ext.Employee");
            DropForeignKey("dbo.OpVisa", "EmployeeId", "ext.Employee");
            DropForeignKey("dbo.Card", "EmployeeId", "ext.Employee");
            DropForeignKey("dbo.RoleAssignment", "NodeId", "dbo.Node");
            DropIndex("dbo.OpVisa", new[] { "EmployeeId" });
            DropIndex("dbo.Card", new[] { "EmployeeId" });
            DropIndex("dbo.EmployeeRoles", new[] { "RoleId" });
            DropIndex("dbo.EmployeeRoles", new[] { "EmployeeId" });
            DropIndex("dbo.RoleAssignment", new[] { "NodeId" });
            DropIndex("dbo.RoleAssignment", new[] { "RoleId" });
            DropColumn("dbo.Card", "EmployeeId");
            DropColumn("dbo.OpVisa", "EmployeeId");
            DropTable("dbo.EmployeeRoles");
            DropTable("dbo.Role");
            DropTable("dbo.RoleAssignment");
            CreateIndex("dbo.Card", "ExternalUserId");
            CreateIndex("dbo.OpVisa", "ExternalUserId");
            AddForeignKey("dbo.OpVisa", "ExternalUserId", "ext.User", "Id");
            AddForeignKey("dbo.Card", "ExternalUserId", "ext.User", "Id");
        }
    }
}
