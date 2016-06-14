namespace ArtCircler.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class populateEventsTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Music')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Photography')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Painting')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Performance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'StandUp')");



        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id In (1, 2, 3, 4,)");
        }
    }
}
