using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Skrabbl.DataAccess.Test
{
    public class ConfigFixture
    {
        private static IConfiguration _config;

        public static IConfiguration Config
        {
            get
            {
                if (_config != null) return _config;

                var configDictionary = new Dictionary<string, string>
                {
                    {
                        "ConnectionStrings:DefaultConnection",
                        "Server=hildur.ucn.dk;Database=dmaa0220_1086234;User Id=dmaa0220_1086234;Password=Password1!;Trusted_Connection=False;"
                    }
                };
                _config = new ConfigurationBuilder()
                    .AddInMemoryCollection(configDictionary)
                    .Build();

                return _config;
            }
        }
    }
}