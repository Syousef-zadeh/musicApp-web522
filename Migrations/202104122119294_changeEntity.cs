namespace Assignment_6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtistMediaItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackContentType = c.String(maxLength: 200),
                        Track = c.Binary(),
                        Artist_Id = c.Int(),
                        Track_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .ForeignKey("dbo.Tracks", t => t.Track_Id)
                .Index(t => t.Artist_Id)
                .Index(t => t.Track_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArtistMediaItems", "Track_Id", "dbo.Tracks");
            DropForeignKey("dbo.ArtistMediaItems", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.ArtistMediaItems", new[] { "Track_Id" });
            DropIndex("dbo.ArtistMediaItems", new[] { "Artist_Id" });
            DropTable("dbo.ArtistMediaItems");
        }
    }
}
