using MangaCatalog.Common.Entities;
using Npgsql;

namespace MangaCatalog.Common.Data
{
    public interface IMangaCatalogContext
    {
        NpgsqlConnection GetConnection();
    }
}
