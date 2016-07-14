namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newisArtist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsArtist", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "Artist");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Artist", c => c.Boolean(nullable: false));
            DropColumn("dbo.AspNetUsers", "IsArtist");
        }
    }
}
