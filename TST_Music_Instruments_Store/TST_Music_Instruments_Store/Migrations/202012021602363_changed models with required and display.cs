namespace TST_Music_Instruments_Store.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodelswithrequiredanddisplay : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "NameOfProduct", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductCategory", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ProductSubCategory", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ImagePath", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Manufacturer", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Color", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Color", c => c.String());
            AlterColumn("dbo.Products", "Manufacturer", c => c.String());
            AlterColumn("dbo.Products", "ImagePath", c => c.String());
            AlterColumn("dbo.Products", "ProductSubCategory", c => c.String());
            AlterColumn("dbo.Products", "ProductCategory", c => c.String());
            AlterColumn("dbo.Products", "NameOfProduct", c => c.String());
        }
    }
}
