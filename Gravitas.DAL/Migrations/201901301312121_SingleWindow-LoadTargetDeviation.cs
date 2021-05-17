namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SingleWindowLoadTargetDeviation : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "LoadTargetDeviationPlus", c => c.Int(nullable: false));
            AddColumn("opd.SingleWindowOpData", "LoadTargetDeviationMinus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "LoadTargetDeviationMinus");
            DropColumn("opd.SingleWindowOpData", "LoadTargetDeviationPlus");
        }
    }
}
