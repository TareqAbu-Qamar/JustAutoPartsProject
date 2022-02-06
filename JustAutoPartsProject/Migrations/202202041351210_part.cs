namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class part : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Cart_ShopCartID", "dbo.ShoppingCarts");
            DropIndex("dbo.Orders", new[] { "Cart_ShopCartID" });
            AddColumn("dbo.Orders", "part_PartID", c => c.Int());
            CreateIndex("dbo.Orders", "part_PartID");
            AddForeignKey("dbo.Orders", "part_PartID", "dbo.Parts", "PartID");
            DropColumn("dbo.Orders", "Cart_ShopCartID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Cart_ShopCartID", c => c.Int());
            DropForeignKey("dbo.Orders", "part_PartID", "dbo.Parts");
            DropIndex("dbo.Orders", new[] { "part_PartID" });
            DropColumn("dbo.Orders", "part_PartID");
            CreateIndex("dbo.Orders", "Cart_ShopCartID");
            AddForeignKey("dbo.Orders", "Cart_ShopCartID", "dbo.ShoppingCarts", "ShopCartID");
        }
    }
}
