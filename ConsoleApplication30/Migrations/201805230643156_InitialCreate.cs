namespace ConsoleApplication30.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Garages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GarageTreatments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Garage_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Garages", t => t.Garage_ID)
                .Index(t => t.Garage_ID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        IDplate = c.String(nullable: false, maxLength: 128),
                        brand = c.String(),
                        color = c.String(),
                        buyingDate = c.DateTime(nullable: false),
                        garage_ID = c.Int(),
                        owner_OwnerID = c.Int(),
                    })
                .PrimaryKey(t => t.IDplate)
                .ForeignKey("dbo.Garages", t => t.garage_ID)
                .ForeignKey("dbo.Owners", t => t.owner_OwnerID)
                .Index(t => t.garage_ID)
                .Index(t => t.owner_OwnerID);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.OwnerID);
            
            CreateTable(
                "dbo.VehicleGarageTreatments",
                c => new
                    {
                        Vehicle_IDplate = c.String(nullable: false, maxLength: 128),
                        GarageTreatment_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vehicle_IDplate, t.GarageTreatment_ID })
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_IDplate, cascadeDelete: true)
                .ForeignKey("dbo.GarageTreatments", t => t.GarageTreatment_ID, cascadeDelete: true)
                .Index(t => t.Vehicle_IDplate)
                .Index(t => t.GarageTreatment_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GarageTreatments", "Garage_ID", "dbo.Garages");
            DropForeignKey("dbo.VehicleGarageTreatments", "GarageTreatment_ID", "dbo.GarageTreatments");
            DropForeignKey("dbo.VehicleGarageTreatments", "Vehicle_IDplate", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "owner_OwnerID", "dbo.Owners");
            DropForeignKey("dbo.Vehicles", "garage_ID", "dbo.Garages");
            DropIndex("dbo.VehicleGarageTreatments", new[] { "GarageTreatment_ID" });
            DropIndex("dbo.VehicleGarageTreatments", new[] { "Vehicle_IDplate" });
            DropIndex("dbo.Vehicles", new[] { "owner_OwnerID" });
            DropIndex("dbo.Vehicles", new[] { "garage_ID" });
            DropIndex("dbo.GarageTreatments", new[] { "Garage_ID" });
            DropTable("dbo.VehicleGarageTreatments");
            DropTable("dbo.Owners");
            DropTable("dbo.Vehicles");
            DropTable("dbo.GarageTreatments");
            DropTable("dbo.Garages");
        }
    }
}
