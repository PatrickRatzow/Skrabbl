using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skrabbl.API.Hubs;
using Skrabbl.API.Middleware;
using Skrabbl.API.Services;
using Skrabbl.API.Services.TimerService;
using Skrabbl.DataAccess;
using Skrabbl.DataAccess.MsSql;
using Skrabbl.DataAccess.MsSql.Queries;

namespace Skrabbl.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddMsSqlRepositories(services);
            AddBusinessLogicServices(services);

            services.AddTokenAuthentication(Configuration);

            services.AddSingleton<TurnTimerService>();

            services.AddSpaStaticFiles(options => { options.RootPath = "wwwroot"; });
            services.AddMemoryCache();
            services.AddControllers();

            services.AddSignalR();
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSqlServer()
                    // Set the connection string
                    .WithGlobalConnectionString(Configuration.GetConnectionString("DefaultConnection"))
                    // Define the assembly containing the migrations
                    .ScanIn(Assembly.Load("Skrabbl.DataAccess")).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<GameHub>("/ws/game");
            });

            app.UseSpaStaticFiles();
            app.UseSpa(builder =>
            {
                if (env.IsDevelopment())
                {
                    builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });

            using var scope = app.ApplicationServices.CreateScope();
            var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
            migrator.MigrateUp();
        }

        private void AddMsSqlRepositories(IServiceCollection services)
        {
            services.AddTransient<ICommandText, CommandText>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IGameLobbyRepository, GameLobbyRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IWordListRepository, WordListRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();

        }

        private void AddBusinessLogicServices(IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICryptographyService, CryptographyService>();
            services.AddScoped<IGameLobbyService, GameLobbyService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IWordService, WordService>();
            services.AddScoped<ITurnTimerService, TurnTimerService>();
        }
    }
}