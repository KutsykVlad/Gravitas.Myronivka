namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BiggerOpRoutineStateName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OpRoutineState", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OpRoutineState", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
