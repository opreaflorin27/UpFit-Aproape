using System.Data.Entity;

namespace UpFit__main.Models
{
    public class CodeFirstDb : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<FoodType> foodTypes { get; set; }
        public DbSet<Food> foods { get; set; }
        public DbSet<MealType> mealTypes { get; set; }
        public DbSet<Meal> meals { get; set; }

        public DbSet<Coach> coaches { get; set; }
        public DbSet<Video> videos { get; set; }
    }
}