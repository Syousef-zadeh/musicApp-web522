namespace Assignment_6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeArtistMedia : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ArtistMediaItems", name: "Track_Id", newName: "Tracks_Id");
            RenameIndex(table: "dbo.ArtistMediaItems", name: "IX_Track_Id", newName: "IX_Tracks_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ArtistMediaItems", name: "IX_Tracks_Id", newName: "IX_Track_Id");
            RenameColumn(table: "dbo.ArtistMediaItems", name: "Tracks_Id", newName: "Track_Id");
        }
    }
}
