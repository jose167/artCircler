namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bioAndArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String(maxLength: 500));
            AddColumn("dbo.AspNetUsers", "Artist", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Artist");
            DropColumn("dbo.AspNetUsers", "Bio");
        }
    }
}
