namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChagneTypeMixedFeedSiloSiloWeight : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MixedFeedSilo", "SiloWeight", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MixedFeedSilo", "SiloWeight", c => c.Long(nullable: false));
        }
    }
}
