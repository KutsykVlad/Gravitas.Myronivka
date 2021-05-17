namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMSFieldTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QueueRegister", "SMSTimeAllowed", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QueueRegister", "SMSTimeAllowed");
        }
    }
}
