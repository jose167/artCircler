namespace ArtCircler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        EventoId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.EventoId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventoId)
                .Index(t => t.EventoId)
                .Index(t => t.AttendeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "EventoId", "dbo.Events");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropIndex("dbo.Attendances", new[] { "EventoId" });
            DropTable("dbo.Attendances");
        }
    }
}
