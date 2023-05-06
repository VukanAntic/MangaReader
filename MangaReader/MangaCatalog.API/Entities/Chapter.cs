namespace MangaCatalog.API.Entities
{
    public class Chapter
    {
        public string Id { get; set; }
        public string MangaId { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
    }
}
