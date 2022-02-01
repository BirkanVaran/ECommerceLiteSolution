namespace ECommerceLiteDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableNameProductsUpdated : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Pruducts", newName: "Products");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Products", newName: "Pruducts");
        }
    }
}
