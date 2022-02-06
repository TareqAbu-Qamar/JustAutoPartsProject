namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class o : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Items", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Items");
        }
    }
}
