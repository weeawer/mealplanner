using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Configurations;

public class ChoiseMealConfiguration : IEntityTypeConfiguration<ChoiseMeal>
{
    public void Configure(EntityTypeBuilder<ChoiseMeal> builder)
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