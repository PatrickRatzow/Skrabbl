using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Skrabbl.DataAccess.MsSql.Test
{
    class ConfigFixture
    {
        public IConfiguration Config
        {
            get
            {
                var configDictionary = new Dictionary<string, string>
                {
                    {
                        "ConnectionStrings:DefaultConnection",
                        "Server=hildur.ucn.dk;Database=dmaa0220_1083739;User Id=dmaa0220_1083739;Password=Password1!;Trusted_Connection=False;"
                    }
                };
                var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(configDictionary)
                    .Build();

                return config;
            }
        }
    }
}