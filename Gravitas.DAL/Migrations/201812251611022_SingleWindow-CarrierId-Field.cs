namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SingleWindowCarrierIdField : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "CarrierId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "CarrierId");
        }
    }
}
