namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLaboratoryCommentCentralLaboratory : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.CentralLabOpData", "LaboratoryComment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.CentralLabOpData", "LaboratoryComment");
        }
    }
}
