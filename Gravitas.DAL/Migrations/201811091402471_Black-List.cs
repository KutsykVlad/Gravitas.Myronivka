namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlackList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "blacklist.Driver",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DriverName = c.String(),
                        DriverSurname = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "blacklist.Partner",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PartnerId = c.String(nullable: false, maxLength: 250),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ext.Partner", t => t.PartnerId, cascadeDelete: true)
                .Index(t => t.PartnerId);
            
            CreateTable(
                "blacklist.Trailer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TrailerNo = c.String(),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("blacklist.Partner", "PartnerId", "ext.Partner");
            DropIndex("blacklist.Partner", new[] { "PartnerId" });
            DropTable("blacklist.Trailer");
            DropTable("blacklist.Partner");
            DropTable("blacklist.Driver");
        }
    }
}
