namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBarCoreSingleWindowOpData : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "BarCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "BarCode");
        }
    }
}
