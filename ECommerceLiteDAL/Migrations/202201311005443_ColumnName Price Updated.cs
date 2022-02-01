namespace ECommerceLiteDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNamePriceUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "UnitPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "Price");
        }
    }
}
