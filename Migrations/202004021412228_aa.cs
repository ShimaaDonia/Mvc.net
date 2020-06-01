namespace E_CommerceProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "AmountPaid", c => c.Double(nullable: false));
            DropColumn("dbo.Orders", "City");
            DropColumn("dbo.Orders", "State");
            DropColumn("dbo.Orders", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Country", c => c.String(maxLength: 10));
            AddColumn("dbo.Orders", "State", c => c.String(maxLength: 10));
            AddColumn("dbo.Orders", "City", c => c.String(maxLength: 10));
            AlterColumn("dbo.Orders", "AmountPaid", c => c.String());
        }
    }
}
