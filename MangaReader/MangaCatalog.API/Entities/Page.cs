namespace MangaCatalog.API.Entities
{
    public class Page
    {
        public string Id { get; set; }
        public string ChapterId { get; set; }
        public int PageNumber { get; set; }
        public string ImageLink { get; set; }
    }
}
