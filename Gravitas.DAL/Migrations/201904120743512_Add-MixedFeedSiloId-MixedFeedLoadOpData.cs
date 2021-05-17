namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMixedFeedSiloIdMixedFeedLoadOpData : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.MixedFeedLoadOpData", "MixedFeedSiloId", c => c.Long());
            CreateIndex("opd.MixedFeedLoadOpData", "MixedFeedSiloId");
            AddForeignKey("opd.MixedFeedLoadOpData", "MixedFeedSiloId", "dbo.MixedFeedSilo", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("opd.MixedFeedLoadOpData", "MixedFeedSiloId", "dbo.MixedFeedSilo");
            DropIndex("opd.MixedFeedLoadOpData", new[] { "MixedFeedSiloId" });
            DropColumn("opd.MixedFeedLoadOpData", "MixedFeedSiloId");
        }
    }
}
