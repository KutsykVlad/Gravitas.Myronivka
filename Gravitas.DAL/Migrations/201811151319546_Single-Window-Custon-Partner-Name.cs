namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SingleWindowCustonPartnerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "CustomPartnerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "CustomPartnerName");
        }
    }
}
