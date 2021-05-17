namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NodeMaximumProcessingtime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Node", "MaximumProcessingTime", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Node", "MaximumProcessingTime");
        }
    }
}
