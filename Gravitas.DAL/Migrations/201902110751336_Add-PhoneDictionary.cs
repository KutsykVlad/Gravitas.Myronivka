namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneDictionary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PhoneDictionary",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Number = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhoneDictionary");
        }
    }
}
