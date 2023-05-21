using MangaCatalog.Common.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace MangaCatalog.Common.Data
{
    public class MangaCatalogContext : IMangaCatalogContext
    {
        private readonly IConfiguration _configuration;
        private NpgsqlConnection _connection;

        public MangaCatalogContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var connStr = _configuration.GetValue<string>("Manga:UrlToDatabase");
            //Console.WriteLine(connStr);
            _connection = new NpgsqlConnection(connStr);
        }

        public NpgsqlConnection GetConnection() {
            return _connection;
        }

    }
}
