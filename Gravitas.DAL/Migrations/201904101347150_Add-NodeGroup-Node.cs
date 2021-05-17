namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNodeGroupNode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Node", "NodeGroup", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Node", "NodeGroup");
        }
    }
}
