namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingvenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "OriginalVenue", c => c.String());
            DropColumn("dbo.Notifications", "OriginalVenu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "OriginalVenu", c => c.String());
            DropColumn("dbo.Notifications", "OriginalVenue");
        }
    }
}
