namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredictionEntranceTimeSingleWindow : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "PredictionEntranceTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "PredictionEntranceTime");
        }
    }
}
