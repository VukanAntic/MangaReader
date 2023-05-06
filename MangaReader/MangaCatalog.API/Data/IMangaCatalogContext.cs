using MangaCatalog.API.Entities;

namespace MangaCatalog.API.Data
{
    public interface IMangaCatalogContext
    {
        List<Manga> Mangas { get; }
    }
}
