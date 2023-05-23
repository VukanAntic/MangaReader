namespace MangaCatalog.Common.Entities
{
    public class AuthorPageResponse
    {
        public Author AuthorInfo { get; set; }
        public IEnumerable<Manga> MangaList { get; set; }
    }
}
