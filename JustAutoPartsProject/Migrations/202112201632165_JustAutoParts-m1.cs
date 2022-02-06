namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JustAutoPartsm1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillID = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Status = c.String(),
                        BillDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BillID);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarID = c.Int(nullable: false, identity: true),
                        ModelName = c.String(),
                        Year = c.Int(nullable: false),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.CarID);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        PartID = c.Int(nullable: false, identity: true),
                        Category = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        DefualtPrice = c.Double(nullable: false),
                        SelectedItem = c.Boolean(nullable: false),
                        Car_CarID = c.Int(),
                        Order_OrderID = c.Int(),
                        ShoppingCart_ShopCartID = c.Int(),
                    })
                .PrimaryKey(t => t.PartID)
                .ForeignKey("dbo.Cars", t => t.Car_CarID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShopCartID)
                .Index(t => t.Car_CarID)
                .Index(t => t.Order_OrderID)
                .Index(t => t.ShoppingCart_ShopCartID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Bill_BillID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Bills", t => t.Bill_BillID)
                .Index(t => t.Bill_BillID);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShopCartID = c.Int(nullable: false, identity: true),
                        PartID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShopCartID)
                .ForeignKey("dbo.Parts", t => t.PartID, cascadeDelete: true)
                .Index(t => t.PartID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false),
                        Role = c.String(),
                        virtualWallet_VirtualWalletID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.VirtualWallets", t => t.virtualWallet_VirtualWalletID)
                .Index(t => t.virtualWallet_VirtualWalletID);
            
            CreateTable(
                "dbo.VirtualWallets",
                c => new
                    {
                        VirtualWalletID = c.Int(nullable: false, identity: true),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.VirtualWalletID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "virtualWallet_VirtualWalletID", "dbo.VirtualWallets");
            DropForeignKey("dbo.ShoppingCarts", "PartID", "dbo.Parts");
            DropForeignKey("dbo.Parts", "ShoppingCart_ShopCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Parts", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Bill_BillID", "dbo.Bills");
            DropForeignKey("dbo.Parts", "Car_CarID", "dbo.Cars");
            DropIndex("dbo.Users", new[] { "virtualWallet_VirtualWalletID" });
            DropIndex("dbo.ShoppingCarts", new[] { "PartID" });
            DropIndex("dbo.Orders", new[] { "Bill_BillID" });
            DropIndex("dbo.Parts", new[] { "ShoppingCart_ShopCartID" });
            DropIndex("dbo.Parts", new[] { "Order_OrderID" });
            DropIndex("dbo.Parts", new[] { "Car_CarID" });
            DropTable("dbo.VirtualWallets");
            DropTable("dbo.Users");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.Orders");
            DropTable("dbo.Parts");
            DropTable("dbo.Cars");
            DropTable("dbo.Bills");
        }
    }
}
