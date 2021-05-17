﻿namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLabManagerTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LabManagerList", "EmployeeId", "ext.Employee");
            DropIndex("dbo.LabManagerList", new[] { "EmployeeId" });
            DropTable("dbo.LabManagerList");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LabManagerList",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.LabManagerList", "EmployeeId");
            AddForeignKey("dbo.LabManagerList", "EmployeeId", "ext.Employee", "Id", cascadeDelete: true);
        }
    }
}