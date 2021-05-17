namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollisionCommentCentralLabOpData : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.CentralLabOpData", "CollisionComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.CentralLabOpData", "CollisionComment");
        }
    }
}
