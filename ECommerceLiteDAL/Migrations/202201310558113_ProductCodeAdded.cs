namespace ECommerceLiteDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCodeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pruducts", "PruductCode", c => c.String(maxLength: 8));
            CreateIndex("dbo.Pruducts", "PruductCode", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Pruducts", new[] { "PruductCode" });
            DropColumn("dbo.Pruducts", "PruductCode");
        }
    }
}
