namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.AccommodationTypes", "Name", unique: true);
            CreateIndex("dbo.Regions", "Name", unique: true);
            CreateIndex("dbo.Countries", "Code", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Countries", new[] { "Code" });
            DropIndex("dbo.Regions", new[] { "Name" });
            DropIndex("dbo.AccommodationTypes", new[] { "Name" });
        }
    }
}
