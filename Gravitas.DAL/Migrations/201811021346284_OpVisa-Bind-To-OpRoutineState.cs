namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpVisaBindToOpRoutineState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OpVisa", "OpRoutineStateId", c => c.Long(nullable: false));
            CreateIndex("dbo.OpVisa", "OpRoutineStateId");
            AddForeignKey("dbo.OpVisa", "OpRoutineStateId", "dbo.OpRoutineState", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OpVisa", "OpRoutineStateId", "dbo.OpRoutineState");
            DropIndex("dbo.OpVisa", new[] { "OpRoutineStateId" });
            DropColumn("dbo.OpVisa", "OpRoutineStateId");
        }
    }
}
