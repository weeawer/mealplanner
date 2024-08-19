using api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace api.Configurations
{
    public class ChoiseMealsSwapConfiguration : IEntityTypeConfiguration<ChoiseMealsSwap>
    {
        public void Configure(EntityTypeBuilder<ChoiseMealsSwap> builder)
        {
            builder
                .HasKey(cm => new { cm.DayId, cm.MealId, cm.AppUserId });

            builder
                .HasOne(cm => cm.AppUser)
                .WithMany(c => c.ChoiseMeals)
                .HasForeignKey(cm => cm.AppUserId);

            builder
                .HasOne(cm => cm.Day)
                .WithMany(c => c.ChoiseMeals)
                .HasForeignKey(cm => cm.DayId);

            builder
                .HasOne(cm => cm.Meal)
                .WithMany(m => m.ChoiseMeals)
                .HasForeignKey(cm => cm.MealId);
        }

       
    }
}
