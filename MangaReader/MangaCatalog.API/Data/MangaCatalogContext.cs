using MangaCatalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MangaCatalog.API.Data
{
    public class MangaCatalogContext : IMangaCatalogContext
    {
        private readonly IConfiguration _configuration;

        public MangaCatalogContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public NpgsqlConnection GetConnection() {
            var connStr = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            return new NpgsqlConnection(connStr);
        }

    }
}
