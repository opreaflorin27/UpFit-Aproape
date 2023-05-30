namespace UpFit__main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Coaches",
                c => new
                    {
                        CoachID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CoachID);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        foodID = c.Int(nullable: false, identity: true),
                        type = c.Int(nullable: false),
                        name = c.String(nullable: false),
                        calories = c.Int(nullable: false),
                        proteins = c.Double(nullable: false),
                        fats = c.Double(nullable: false),
                        carbs = c.Double(nullable: false),
                        fibers = c.Double(nullable: false),
                        vitamin_A = c.Double(nullable: false),
                        vitamin_B = c.Double(nullable: false),
                        vitamin_C = c.Double(nullable: false),
                        vitamin_D = c.Double(nullable: false),
                        vitamin_E = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.foodID);
            
            CreateTable(
                "dbo.FoodTypes",
                c => new
                    {
                        ID_Type = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID_Type);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        mealID = c.Int(nullable: false, identity: true),
                        mealTypeFK = c.Int(nullable: false),
                        userFK = c.Int(nullable: false),
                        foodFK = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                        KcalMeal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.mealID);
            
            CreateTable(
                "dbo.MealTypes",
                c => new
                    {
                        mealTypeID = c.Int(nullable: false, identity: true),
                        mealName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.mealTypeID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        SubscriptionTypeFK = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        KcalDaily = c.Int(nullable: false),
                        Lifestyle = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        videoID = c.Int(nullable: false, identity: true),
                        Vname = c.String(),
                        Vpath = c.String(),
                    })
                .PrimaryKey(t => t.videoID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Videos");
            DropTable("dbo.Users");
            DropTable("dbo.MealTypes");
            DropTable("dbo.Meals");
            DropTable("dbo.FoodTypes");
            DropTable("dbo.Foods");
            DropTable("dbo.Coaches");
            DropTable("dbo.Admins");
        }
    }
}
