namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegisterDateTimeField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreRegisterQueue", "RegisterDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreRegisterQueue", "RegisterDateTime");
        }
    }
}
