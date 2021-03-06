namespace Football.Extensions
{
    using Football.Core.Contracts;
    using Football.Core.Services.Cities;
    using Football.Core.Services.Leagues;
    using Football.Core.Services.Managers;
    using Football.Core.Services.Players;
    using Football.Core.Services.Positions;
    using Football.Core.Services.Stadiums;
    using Football.Core.Services.Statistics;
    using Football.Core.Services.Teams;
    using Football.Infrastructure.Data;
    using Football.Infrastructure.Repositories;
    using Football.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    public static class ServiceCollectionExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder services)
        {
            using var scopedServices = services.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<FootballDbContext>();

            //data.Database.Migrate();

            SeedPosition(data);

            return services;
        }

        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IFootballDbRepository, FootballDbRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IStadiumService, StadiumService>();


            //services.ConfigureApplicationCookie(option =>
            //{
            //    option.LoginPath = $"/Admin/Home/Index";
            //});

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<FootballDbContext>(options =>
                 options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();


            return services;
        }

        private static void SeedPosition(FootballDbContext data)
        {
            if (data.Positions.Any())
            {
                return;
            }

            //data.Positions.AddRange(new[]
            //{
            //    new Position { Name = "Goalkeeper"},
            //    new Position { Name = "Defender - Centre-back"},
            //    new Position { Name = "Defender - Sweeper"},
            //    new Position { Name = "Defender - Full-back"},
            //    new Position { Name = "Defender - Wing-back"},
            //    new Position{ Name = "Midfielder - Central"},
            //    new Position{ Name = "Midfielder - Defensive"},
            //    new Position{ Name = "Midfielder - Attacking"},
            //    new Position{ Name = "Midfielder - Wide"},
            //    new Position { Name = "Forward - Second striker"},
            //    new Position { Name = "Forward - Centre forward"},
            //    new Position { Name = "Forward - Winger"},
            //});

            //data.SaveChanges();
        }
    }
}