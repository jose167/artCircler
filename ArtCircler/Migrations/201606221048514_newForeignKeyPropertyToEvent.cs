namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newForeignKeyPropertyToEvent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Artist_Id" });
            RenameColumn(table: "dbo.Events", name: "Genre_Id", newName: "GenreId");
            RenameIndex(table: "dbo.Events", name: "IX_Genre_Id", newName: "IX_GenreId");
            AddColumn("dbo.Events", "IdArtist", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Artist_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Events", "Artist_Id");
            AddForeignKey("dbo.Events", "Artist_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Artist_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "Artist_Id" });
            AlterColumn("dbo.Events", "Artist_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Events", "IdArtist");
            RenameIndex(table: "dbo.Events", name: "IX_GenreId", newName: "IX_Genre_Id");
            RenameColumn(table: "dbo.Events", name: "GenreId", newName: "Genre_Id");
            CreateIndex("dbo.Events", "Artist_Id");
            AddForeignKey("dbo.Events", "Artist_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
