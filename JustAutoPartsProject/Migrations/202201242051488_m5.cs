namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Cart_ShopCartID", c => c.Int());
            AddColumn("dbo.Users", "Cart_ShopCartID", c => c.Int());
            AlterColumn("dbo.Bills", "Value", c => c.Double(nullable: false));
            CreateIndex("dbo.Orders", "Cart_ShopCartID");
            CreateIndex("dbo.Users", "Cart_ShopCartID");
            AddForeignKey("dbo.Orders", "Cart_ShopCartID", "dbo.ShoppingCarts", "ShopCartID");
            AddForeignKey("dbo.Users", "Cart_ShopCartID", "dbo.ShoppingCarts", "ShopCartID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Cart_ShopCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Orders", "Cart_ShopCartID", "dbo.ShoppingCarts");
            DropIndex("dbo.Users", new[] { "Cart_ShopCartID" });
            DropIndex("dbo.Orders", new[] { "Cart_ShopCartID" });
            AlterColumn("dbo.Bills", "Value", c => c.String());
            DropColumn("dbo.Users", "Cart_ShopCartID");
            DropColumn("dbo.Orders", "Cart_ShopCartID");
        }
    }
}
