using MangaCatalog.API.Entities;

namespace MangaCatalog.API.Data
{
    public class MangaCatalogContext : IMangaCatalogContext
    {
        public List<Manga> Mangas { get; }
    }
}
