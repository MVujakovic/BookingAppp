namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifikovanModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "appUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Accomodations", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Accomodations", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.AccommodationTypes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Comments", "Text", c => c.String(maxLength: 200));
            AlterColumn("dbo.AppUsers", "FullName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Places", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false, maxLength: 20));
            //CreateIndex("dbo.Countries", "Code", unique: true);
            CreateIndex("dbo.AspNetUsers", "appUserId");
            AddForeignKey("dbo.AspNetUsers", "appUserId", "dbo.AppUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.AppUsers", "Username");
            DropColumn("dbo.AppUsers", "Email");
            DropColumn("dbo.AppUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "Password", c => c.String());
            AddColumn("dbo.AppUsers", "Email", c => c.String());
            AddColumn("dbo.AppUsers", "Username", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "appUserId", "dbo.AppUsers");
            DropIndex("dbo.AspNetUsers", new[] { "appUserId" });
            DropIndex("dbo.Countries", new[] { "Code" });
            AlterColumn("dbo.Countries", "Code", c => c.String());
            AlterColumn("dbo.Places", "Name", c => c.String());
            AlterColumn("dbo.AppUsers", "FullName", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "Text", c => c.String());
            AlterColumn("dbo.AccommodationTypes", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Accomodations", "Description", c => c.String());
            AlterColumn("dbo.Accomodations", "Name", c => c.String());
            DropColumn("dbo.AspNetUsers", "appUserId");
        }
    }
}
