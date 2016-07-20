namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventName", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EventName");
        }
    }
}
