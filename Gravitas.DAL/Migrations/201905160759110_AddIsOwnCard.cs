namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsOwnCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Card", "IsOwn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Card", "IsOwn");
        }
    }
}
