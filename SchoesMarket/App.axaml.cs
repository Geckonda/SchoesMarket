using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoesMarket.DAL;
using SchoesMarket.DAL.Repository;
using SchoesMarket.Navigation;
using SchoesMarket.ViewModels;
using SchoesMarket.Views;
using ShoesMarket.Domain.Abstractions;
using ShoesMarket.Domain.Helpers;
using System;
using System.Linq;

namespace SchoesMarket
{
    public partial class App : Application
    {
        // Статическое свойство для глобального доступа к контейнеру зависимостей
        // Инициализировано как null! обещание компилятору, что к моменту использования оно не будет null
        public static IServiceProvider Services { get; private set; } = null!;

        // Статическая переменная для хранения роли текущего пользователя в приложении
        public static UserRole CurrentUserRole = UserRole.None;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        // Настройка контейнера зависимостей (Dependency Injection)
        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("User ID = postgres; database = SchoesMarket; HOST = localhost; Port = 5432; Password = 2245;"));

            // Регистрация универсального репозитория для работы с любыми сущностями
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            // Регистрация специализированного репозитория для пользователей
            services.AddScoped<IUserRepository, UserRepository>();

            // Регистрация сервиса навигации как Singleton (один экземпляр на всё приложение)
            services.AddSingleton<INavigationService>(provider =>
            {
                // Получаем доступ к desktop приложению для управления окнами
                var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
                return new NavigationService(desktop, provider); // Создаем сервис навигации
            });

            // Регистрация ViewModel'ей как Transient (новый экземпляр при каждом запросе)
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ProductCardViewModel>();

            // Регистрация окон как Transient
            services.AddTransient<LoginWindow>();
            services.AddTransient<MainWindow>();
            services.AddTransient<SaveProductWindow>();

            return services; // Возвращаем настроенную коллекцию сервисов
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Строим контейнер зависимостей и сохраняем в статическое свойство
            Services = ConfigureServices().BuildServiceProvider();

            // Проверяем, что приложение запущено в desktop режиме
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                DisableAvaloniaDataAnnotationValidation();

                // Получаем сервис навигации из контейнера и перенаправляем на окно логина
                var navigation = Services.GetRequiredService<INavigationService>();
                navigation.NavigateToLogin();
            }

            base.OnFrameworkInitializationCompleted();
        }


        // Этот метод не изменяется
        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}