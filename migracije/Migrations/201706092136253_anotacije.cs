namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anotacije : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccommodationTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Regions", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.Regions", "Name", c => c.String());
            AlterColumn("dbo.AccommodationTypes", "Name", c => c.String());
        }
    }
}
