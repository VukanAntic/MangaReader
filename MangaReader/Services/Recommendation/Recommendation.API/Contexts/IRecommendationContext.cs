using MangaCatalog.Common.DTOs.Manga;

namespace Recommendation.API.Contexts
{
    public interface IRecommendationContext
    {
        Task<string> GetFavouriteAuthorId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList);
        Task<string> GetFavouriteGenreId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList);
    }
}
