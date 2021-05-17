namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Smstemplates : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SmsTemplates",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(maxLength: 50),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SmsTemplates");
        }
    }
}
