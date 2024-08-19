using api.Data;

namespace api.Service
{
    public class UpdateDaysService : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public UpdateDaysService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Запускаем таймер, который срабатывает каждую минуту
            _timer = new Timer(UpdateDates, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void UpdateDates(object state)
        {
            var currentTime = DateTime.Now;

            // Проверяем, что сейчас понедельник и время 09:35
            if (currentTime.DayOfWeek == DayOfWeek.Monday && currentTime.Hour == 12 && currentTime.Minute == 23)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Получаем все записи с id 1, 2, 3, 4, 5
                    var days = context.Days.Where(d => d.Id >= 1 && d.Id <= 5).ToList();

                    // Определяем даты для следующей недели
                    var nextMonday = currentTime.AddDays(7 - (int)currentTime.DayOfWeek + (int)DayOfWeek.Monday);

                    days[0].Date = nextMonday;                         // Понедельник
                    days[1].Date = nextMonday.AddDays(1);              // Вторник
                    days[2].Date = nextMonday.AddDays(2);              // Среда
                    days[3].Date = nextMonday.AddDays(3);              // Четверг
                    days[4].Date = nextMonday.AddDays(4);              // Пятница

                    // Сохраняем изменения в базе данных
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
