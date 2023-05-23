namespace MangaCatalog.Common.DTOs.Manga
{
    public class BaseMangaDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CoverArt { get; set; }
        public bool ContentRating { get; set; }
        public string AuthorId { get; set; }
    }
}
