using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoesMarket.DAL;
using SchoesMarket.DAL.Repository;
using SchoesMarket.ViewModels;
using SchoesMarket.Views;
using ShoesMarket.Domain.Abstractions;
using System;
using System.Linq;

namespace SchoesMarket
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql("User ID = postgres; database = SchoesMarket; HOST = localhost; Port = 5432; Password = 2245;"));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();


            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ProductCardViewModel>();


            services.AddTransient<LoginWindow>();

            return services;
        }

        public override void OnFrameworkInitializationCompleted()
        {
            // Получаем -> билдим и присваиваемаем наши сервисы в глобальную переменную 
            Services = ConfigureServices().BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {

                DisableAvaloniaDataAnnotationValidation();

                // Создаем окно авторизации
                var loginWindow = new LoginWindow();
                var loginViewModel = Services.GetRequiredService<LoginViewModel>();
                loginWindow.DataContext = loginViewModel;

                // Подписываемся на событие успешной авторизации
                loginViewModel.LoginSuccessful += () =>
                {
                    // Создаем главное окно и передаем пользователя
                    var mainWindow = new MainWindow
                    {
                        DataContext = Services.GetRequiredService<MainWindowViewModel>(),
                    };

                    desktop.MainWindow = mainWindow;
                    mainWindow.Show();
                    loginWindow.Close();
                };

                loginWindow.Show();
                desktop.MainWindow = loginWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }

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