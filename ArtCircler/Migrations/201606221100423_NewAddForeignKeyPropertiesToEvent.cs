namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewAddForeignKeyPropertiesToEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Artist_Id" });
            RenameColumn(table: "dbo.Events", name: "Artist_Id", newName: "ArtistId");
            AlterColumn("dbo.Events", "ArtistId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Events", "ArtistId");
            AddForeignKey("dbo.Events", "ArtistId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Events", "IdArtist");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "IdArtist", c => c.String(nullable: false));
            DropForeignKey("dbo.Events", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "ArtistId" });
            AlterColumn("dbo.Events", "ArtistId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Events", name: "ArtistId", newName: "Artist_Id");
            CreateIndex("dbo.Events", "Artist_Id");
            AddForeignKey("dbo.Events", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
