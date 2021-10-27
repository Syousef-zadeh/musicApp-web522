namespace Assignment_6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMediaItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArtistMediaItems", "Timestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.ArtistMediaItems", "StringId", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.ArtistMediaItems", "Content", c => c.Binary());
            AddColumn("dbo.ArtistMediaItems", "ContentType", c => c.String(maxLength: 200));
            AddColumn("dbo.ArtistMediaItems", "Caption", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.ArtistMediaItems", "MediaContentType");
            DropColumn("dbo.ArtistMediaItems", "Media");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArtistMediaItems", "Media", c => c.Binary());
            AddColumn("dbo.ArtistMediaItems", "MediaContentType", c => c.String(maxLength: 200));
            DropColumn("dbo.ArtistMediaItems", "Caption");
            DropColumn("dbo.ArtistMediaItems", "ContentType");
            DropColumn("dbo.ArtistMediaItems", "Content");
            DropColumn("dbo.ArtistMediaItems", "StringId");
            DropColumn("dbo.ArtistMediaItems", "Timestamp");
        }
    }
}
