namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parts", "Image");
        }
    }
}
