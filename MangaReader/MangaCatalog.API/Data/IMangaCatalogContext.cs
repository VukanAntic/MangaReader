using MangaCatalog.API.Entities;
using Npgsql;

namespace MangaCatalog.API.Data
{
    public interface IMangaCatalogContext
    {
        NpgsqlConnection GetConnection();
    }
}
