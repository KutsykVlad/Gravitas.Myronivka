namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEffectiveClassCollisionCommentLab : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.LabFacelessOpData", "CollisionComment", c => c.String());
            AddColumn("opd.LabFacelessOpData", "LabEffectiveClassifier", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.LabFacelessOpData", "LabEffectiveClassifier");
            DropColumn("opd.LabFacelessOpData", "CollisionComment");
        }
    }
}
