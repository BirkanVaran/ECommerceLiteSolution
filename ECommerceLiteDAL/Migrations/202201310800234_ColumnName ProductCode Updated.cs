namespace ECommerceLiteDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnNameProductCodeUpdated : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Pruducts", new[] { "PruductCode" });
            AddColumn("dbo.Pruducts", "ProductCode", c => c.String(maxLength: 8));
            CreateIndex("dbo.Pruducts", "ProductCode", unique: true);
            DropColumn("dbo.Pruducts", "PruductCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pruducts", "PruductCode", c => c.String(maxLength: 8));
            DropIndex("dbo.Pruducts", new[] { "ProductCode" });
            DropColumn("dbo.Pruducts", "ProductCode");
            CreateIndex("dbo.Pruducts", "PruductCode", unique: true);
        }
    }
}
