namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewColumnsSingleWindowOpData : DbMigration
    {
        public override void Up()
        {
            AddColumn("opd.SingleWindowOpData", "HiredTransportNumber", c => c.String(maxLength: 250));
            AddColumn("opd.SingleWindowOpData", "OriginalТТN", c => c.String());
            AddColumn("opd.SingleWindowOpData", "HiredTrailerNumber", c => c.String());
            AddColumn("opd.SingleWindowOpData", "ContractCarrierId", c => c.String());
            AddColumn("opd.SingleWindowOpData", "SenderWeight", c => c.Double());
            DropColumn("opd.SingleWindowOpData", "HiredTansportNumber");
        }
        
        public override void Down()
        {
            AddColumn("opd.SingleWindowOpData", "HiredTansportNumber", c => c.String(maxLength: 250));
            DropColumn("opd.SingleWindowOpData", "SenderWeight");
            DropColumn("opd.SingleWindowOpData", "ContractCarrierId");
            DropColumn("opd.SingleWindowOpData", "HiredTrailerNumber");
            DropColumn("opd.SingleWindowOpData", "OriginalТТN");
            DropColumn("opd.SingleWindowOpData", "HiredTransportNumber");
        }
    }
}
