
using System;
using System.Windows;
using CoJourney.App.Extensions;
using CoJourney.App.Services;
using CoJourney.App.Settings;
using CoJourney.App.ViewModels;
using CoJourney.BL;
using CoJourney.DAL;
using CoJourney.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


namespace CoJourney.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
                .Build();
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
        {
            builder.AddJsonFile(@"appsettings.json", false, false);
        }

        private static void ConfigureServices(IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddBLServices();

            services.Configure<DALSettings>(configuration.GetSection("CoJourney:DAL"));


            services.AddSingleton<IDbContextFactory<CoJourneyDbContext>>(provider =>
            {
                var dalSettings = provider.GetRequiredService<IOptions<DALSettings>>().Value;
                return new SqlServerDbContextFactory(dalSettings.ConnectionString!, dalSettings.SkipMigrationAndSeedDemoData);
            });

            services.AddSingleton<Views.LoginWindow>();
            services.AddSingleton<MainWindow>();

            //services.AddSingleton<IMessageDialogService, MessageDialogService>();
            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IUserListViewModel, UserListViewModel>();
            services.AddSingleton<ICarListViewModel, CarListViewModel>();
            services.AddSingleton<IJourneyListViewModel, JourneyListViewModel>();
            services.AddSingleton<ICarEventListViewModel, CarEventListViewModel>();
            services.AddSingleton<IInvitationListViewModel, InvitationListViewModel>();
            services.AddFactory<IUserDetailViewModel, UserDetailViewModel>();
            services.AddFactory<ICarDetailViewModel, CarDetailViewModel>();

            services.AddSingleton<LoginWindowViewModel>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var dbContextFactory = _host.Services.GetRequiredService<IDbContextFactory<CoJourneyDbContext>>();

            var dalSettings = _host.Services.GetRequiredService<IOptions<DALSettings>>().Value;

            await using (var dbx = await dbContextFactory.CreateDbContextAsync())
            {
                if (dalSettings.SkipMigrationAndSeedDemoData)
                {
                    await dbx.Database.EnsureDeletedAsync();
                    await dbx.Database.EnsureCreatedAsync();
                }
                else
                {
                    await dbx.Database.MigrateAsync();
                }
            }
            _host.Services.GetRequiredService<MainWindow>();
            //mainWindow.Show(); !!Ne
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

    }
}
