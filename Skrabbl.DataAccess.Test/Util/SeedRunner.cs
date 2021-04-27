using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Skrabbl.DataAccess.Test;

namespace SboxTerror.DataAccess.Test
{
    internal class SeedRunner : IAsyncDisposable
    {
        private List<Seed> _seeds;
        private readonly IConfiguration _configuration;
        private SqlConnection _connection;

        internal SeedRunner(IConfiguration configuration)
        {
            _configuration = configuration;

            FindSeeds();
        }

        internal async Task CleanUp()
        {
            await Connect();

            // Flip it around, so the last seed is the first to be cleaned up
            var seeds = _seeds.OrderByDescending(s =>
                (Attribute.GetCustomAttribute(s.GetType(), typeof(SeedAttribute)) as SeedAttribute)!.Order
            );
            foreach (var seed in seeds)
            {
                seed.Connection = _connection;
                await seed.Down();
            }
        }

        internal async Task Start()
        {
            await Connect();

            DbTransaction trx = null;
            try
            {
                trx = await _connection.BeginTransactionAsync();
                foreach (var seed in _seeds)
                {
                    seed.Connection = _connection;
                    seed.Transaction = trx;
                    await seed.Up();
                }

                await trx.CommitAsync();
            }
            catch (Exception e)
            {
                await trx!.RollbackAsync();

                throw new SeedingException($"Failed to seed! Rolling back", e);
            }
        }

        private async Task Connect()
        {
            if (_connection != null) return;

            _connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await _connection.OpenAsync();
        }

        private void FindSeeds()
        {
            _seeds ??= AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsSubclassOf(typeof(Seed)) && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<Seed>()
                .Where(x => Attribute.GetCustomAttribute(x.GetType(), typeof(SeedAttribute)) != null)
                .OrderBy(x =>
                {
                    var attr = Attribute.GetCustomAttribute(x.GetType(), typeof(SeedAttribute)) as SeedAttribute;

                    return attr!.Order;
                })
                .ToList();
        }

        public ValueTask DisposeAsync()
        {
            return _connection?.DisposeAsync() ?? default;
        }
    }
}