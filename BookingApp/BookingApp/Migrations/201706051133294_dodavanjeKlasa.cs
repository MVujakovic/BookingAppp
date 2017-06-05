namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodavanjeKlasa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        AverageGrade = c.Double(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ImageUrl = c.String(),
                        Approved = c.Boolean(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        AccomodationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccommodationTypes", t => t.AccomodationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.OwnerId)
                .Index(t => t.AccomodationTypeId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Double(nullable: false),
                        Text = c.String(),
                        AccomodationId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .Index(t => t.AccomodationId)
                .Index(t => t.AppUserId);
            
            CreateTable(
                "dbo.RoomReservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AppUserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.AppUserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.Int(nullable: false),
                        BedCount = c.Int(nullable: false),
                        Description = c.String(),
                        PricePerNight = c.Double(nullable: false),
                        AccomodationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .Index(t => t.AccomodationId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AppUsers", "Username", c => c.String());
            AddColumn("dbo.AppUsers", "Email", c => c.String());
            AddColumn("dbo.AppUsers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accomodations", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Accomodations", "OwnerId", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.RoomReservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.RoomReservations", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.Accomodations", "AccomodationTypeId", "dbo.AccommodationTypes");
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Places", new[] { "RegionId" });
            DropIndex("dbo.Rooms", new[] { "AccomodationId" });
            DropIndex("dbo.RoomReservations", new[] { "RoomId" });
            DropIndex("dbo.RoomReservations", new[] { "AppUserId" });
            DropIndex("dbo.Comments", new[] { "AppUserId" });
            DropIndex("dbo.Comments", new[] { "AccomodationId" });
            DropIndex("dbo.Accomodations", new[] { "AccomodationTypeId" });
            DropIndex("dbo.Accomodations", new[] { "OwnerId" });
            DropIndex("dbo.Accomodations", new[] { "PlaceId" });
            DropColumn("dbo.AppUsers", "Password");
            DropColumn("dbo.AppUsers", "Email");
            DropColumn("dbo.AppUsers", "Username");
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
            DropTable("dbo.Places");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomReservations");
            DropTable("dbo.Comments");
            DropTable("dbo.Accomodations");
            DropTable("dbo.AccommodationTypes");
        }
    }
}
