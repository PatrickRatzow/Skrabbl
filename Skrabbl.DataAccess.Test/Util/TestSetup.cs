using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors;
using FluentMigrator.Runner.Processors.SqlServer;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SboxTerror.DataAccess.Test;

namespace Skrabbl.DataAccess.Test
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public async Task Setup()
        {
            Migrate();

            var seedRunner = new SeedRunner(ConfigFixture.Config);
            await seedRunner.CleanUp();
            await seedRunner.Start();
        }

        private void Migrate()
        {
            var connectionString = ConfigFixture.Config.GetConnectionString("DefaultConnection");
            var announcer = new TextWriterAnnouncer(Console.WriteLine);
            announcer.ShowSql = true;

            var assembly = Assembly.Load("Skrabbl.DataAccess");
            var migrationContext = new RunnerContext(announcer);
            var options = new ProcessorOptions
            {
                PreviewOnly = false,
                Timeout = TimeSpan.FromMinutes(1)
            };
            var factory = new SqlServer2016ProcessorFactory();
            using var processor = factory.Create(connectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runner.MigrateUp();
        }
    }
}