namespace JustAutoPartsProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parts", "DefualtPrice", c => c.Double());
            AlterColumn("dbo.Parts", "SelectedItem", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parts", "SelectedItem", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Parts", "DefualtPrice", c => c.Double(nullable: false));
        }
    }
}
