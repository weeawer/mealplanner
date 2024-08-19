using api.Configurations;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<ChoiseMeal> ChoiseMeals { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ChoiseMealsSwap> ChoiseMealsSwaps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Добавляем роли
            List<IdentityRole> roles = new List<IdentityRole>
    {
        new IdentityRole
        {
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
            Name = "User",
            NormalizedName = "USER"
        },
    };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Применение конфигурации ChoiseMealsSwap
            modelBuilder.ApplyConfiguration(new ChoiseMealsSwapConfiguration());

            // Определение бесключевой сущности ChoiseMeal
            modelBuilder.Entity<ChoiseMeal>()
                .HasKey(cm => new { cm.AppUserId, cm.DayId, cm.MealId });
        }

    }
}