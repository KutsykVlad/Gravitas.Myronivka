namespace Gravitas.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductFullNameMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("ext.Product", "ShortName", c => c.String(maxLength: 500));
            AlterColumn("ext.Product", "FullName", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("ext.Product", "FullName", c => c.String(maxLength: 250));
            AlterColumn("ext.Product", "ShortName", c => c.String(maxLength: 250));
        }
    }
}
