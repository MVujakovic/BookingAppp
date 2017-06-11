namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Regions", "Name", unique: true);
            CreateIndex("dbo.Countries", "Code", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Countries", new[] { "Code" });
            DropIndex("dbo.Regions", new[] { "Name" });
        }
    }
}
