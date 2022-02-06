namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parts", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parts", "Image", c => c.Binary());
        }
    }
}
