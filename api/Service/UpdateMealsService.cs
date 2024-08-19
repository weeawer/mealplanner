using api.Data;
using api.Models;

namespace api.Service
{
    public class UpdateMealsService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public UpdateMealsService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Проверяем каждую минуту
            _timer = new Timer(UpdateMeals, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void UpdateMeals(object state)
        {
            var currentTime = DateTime.Now;

            // Проверка на конец пятницы (16:00)
            if (currentTime.DayOfWeek == DayOfWeek.Monday && currentTime.Hour == 17 && currentTime.Minute == 03)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Очистка текущих данных в ChoiseMeals
                    context.ChoiseMeals.RemoveRange(context.ChoiseMeals);

                    // Получаем все данные из ChoiseMealsSwap
                    var swapMeals = context.ChoiseMealsSwaps.ToList();

                    // Переносим данные в ChoiseMeals
                    foreach (var swapMeal in swapMeals)
                    {
                        context.ChoiseMeals.Add(new ChoiseMeal
                        {
                            AppUserId = swapMeal.AppUserId,
                            DayId = swapMeal.DayId,
                            MealId = swapMeal.MealId
                        });
                    }

                    // Сохраняем изменения в базе данных
                    context.SaveChanges();

                    // Очищаем данные в ChoiseMealsSwap после переноса
                    context.ChoiseMealsSwaps.RemoveRange(swapMeals);
                    context.SaveChanges();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
