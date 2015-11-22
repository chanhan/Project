namespace GuestBookMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addicon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Icon", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "Icon");
        }
    }
}
