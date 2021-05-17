namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScaleOpDataColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.ScaleOpData", "TrailerAvailable", c => c.Boolean(nullable: false));
            AddColumn("opd.ScaleOpData", "GuardPresence", c => c.Boolean(nullable: false));
            AlterColumn("dbo.OpRoutineTransition", "Name", c => c.String(nullable: false, maxLength: 80));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OpRoutineTransition", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("opd.ScaleOpData", "GuardPresence");
            DropColumn("opd.ScaleOpData", "TrailerAvailable");
        }
    }
}
