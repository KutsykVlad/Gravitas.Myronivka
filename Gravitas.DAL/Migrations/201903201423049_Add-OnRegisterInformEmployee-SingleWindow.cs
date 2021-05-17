namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOnRegisterInformEmployeeSingleWindow : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "OnRegisterInformEmployee", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("opd.SingleWindowOpData", "OnRegisterInformEmployee");
        }
    }
}
