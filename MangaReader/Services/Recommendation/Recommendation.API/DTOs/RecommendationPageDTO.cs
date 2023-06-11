using MangaCatalog.Common.DTOs.Manga;

namespace Recommendation.API.DTOs
{
    public class RecommendationPageDTO
    {
        public string FavouriteAuthorName { get; set; }
        public IEnumerable<MangaDTO> MangasByFavouriteAuthor { get; set; }
        public string FavouriteGenreName { get; set; }
        public IEnumerable<MangaDTO> MangasFromFavouriteGenre { get; set; }
        public IEnumerable<MangaDTO> ReadMangas { get; set; }
        public IEnumerable<MangaDTO> WishListMangas { get; set; }
    }
}
