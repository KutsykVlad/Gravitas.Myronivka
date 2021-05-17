namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParentCardIdCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Card", "ParentCardId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Card", "ParentCardId");
        }
    }
}
