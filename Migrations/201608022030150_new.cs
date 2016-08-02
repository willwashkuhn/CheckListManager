namespace CheckListManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CheckListItems");
            AlterColumn("dbo.CheckListItems", "Order", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CheckListItems", new[] { "CheckListId", "Order" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CheckListItems");
            AlterColumn("dbo.CheckListItems", "Order", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CheckListItems", new[] { "CheckListId", "Order" });
        }
    }
}
