namespace MangaCatalog.API.DTOs
{
    public class DescriptiveMangaDTO : BaseMangaDTO
    {
        public string Description { get; set; }
        public string Status { get; set; }
        public bool ContentRating { get; set; }
        public string AuthorId { get; set; }
    }
}
