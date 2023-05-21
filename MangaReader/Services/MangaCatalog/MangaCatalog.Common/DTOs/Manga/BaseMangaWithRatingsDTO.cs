
namespace MangaCatalog.Common.DTOs.Manga
{
    public class BaseMangaWithRatingsDTO : BaseIdentityMangaDTO
    {
        public int NumberOfRatings { get; set; }
        public float SumOfRatings { get; set; }
    }
}
