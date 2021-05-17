namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeePhoneAssigment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhoneInformTicketAssignment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PhoneDictionaryId = c.Long(nullable: false),
                        TicketId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PhoneDictionary", t => t.PhoneDictionaryId, cascadeDelete: true)
                .ForeignKey("dbo.Ticket", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.PhoneDictionaryId)
                .Index(t => t.TicketId);
            
            AddColumn("dbo.PhoneDictionary", "PhoneNumber", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.PhoneDictionary", "EmployeePosition", c => c.String());
            AddColumn("dbo.PhoneDictionary", "IsVisibleForSingleWindow", c => c.Boolean(nullable: false));
            DropColumn("opd.SingleWindowOpData", "OnRegisterInformEmployee");
            DropColumn("dbo.PhoneDictionary", "Number");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PhoneDictionary", "Number", c => c.String(nullable: false, maxLength: 20));
            AddColumn("opd.SingleWindowOpData", "OnRegisterInformEmployee", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.PhoneInformTicketAssignment", "TicketId", "dbo.Ticket");
            DropForeignKey("dbo.PhoneInformTicketAssignment", "PhoneDictionaryId", "dbo.PhoneDictionary");
            DropIndex("dbo.PhoneInformTicketAssignment", new[] { "TicketId" });
            DropIndex("dbo.PhoneInformTicketAssignment", new[] { "PhoneDictionaryId" });
            DropColumn("dbo.PhoneDictionary", "IsVisibleForSingleWindow");
            DropColumn("dbo.PhoneDictionary", "EmployeePosition");
            DropColumn("dbo.PhoneDictionary", "PhoneNumber");
            DropTable("dbo.PhoneInformTicketAssignment");
        }
    }
}
