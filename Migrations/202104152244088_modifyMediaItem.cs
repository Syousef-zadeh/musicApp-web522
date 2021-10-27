namespace Assignment_6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyMediaItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistMediaItems", "Track_Id", "dbo.Tracks");
            DropIndex("dbo.ArtistMediaItems", new[] { "Track_Id" });
            DropColumn("dbo.ArtistMediaItems", "Track_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArtistMediaItems", "Track_Id", c => c.Int());
            CreateIndex("dbo.ArtistMediaItems", "Track_Id");
            AddForeignKey("dbo.ArtistMediaItems", "Track_Id", "dbo.Tracks", "Id");
        }
    }
}
