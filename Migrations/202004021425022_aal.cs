namespace E_CommerceProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "QuantityInOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carts", "QuantityInOrder");
        }
    }
}
