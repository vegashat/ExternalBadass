namespace ExternalBadass.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PointValue = c.Int(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Goals",
                c => new
                    {
                        GoalId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GoalId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FullName = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        MeasurementId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BMI = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MeasurementId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Incentives",
                c => new
                    {
                        IncentiveId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PointTotal = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.IncentiveId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Incentives", new[] { "UserId" });
            DropIndex("dbo.Measurements", new[] { "UserId" });
            DropIndex("dbo.Goals", new[] { "ActivityId" });
            DropIndex("dbo.Goals", new[] { "UserId" });
            DropIndex("dbo.Activities", new[] { "User_UserId" });
            DropForeignKey("dbo.Incentives", "UserId", "dbo.Users");
            DropForeignKey("dbo.Measurements", "UserId", "dbo.Users");
            DropForeignKey("dbo.Goals", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Goals", "UserId", "dbo.Users");
            DropForeignKey("dbo.Activities", "User_UserId", "dbo.Users");
            DropTable("dbo.Incentives");
            DropTable("dbo.Measurements");
            DropTable("dbo.Users");
            DropTable("dbo.Goals");
            DropTable("dbo.Activities");
        }
    }
}
