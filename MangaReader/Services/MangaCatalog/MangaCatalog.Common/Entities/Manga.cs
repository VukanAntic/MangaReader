namespace MangaCatalog.Common.Entities
{
    public class Manga
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CoverArt { get; set; }
        public bool ContentRating { get; set; }
        public string AuthorId { get; set; }
        public int NumberOfRatings { get; set; }
        public float SumOfRatings { get; set; }
    }
}
