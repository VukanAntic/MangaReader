using MangaCatalog.Common.DTOs.Manga;

namespace Recommendation.API.Contexts
{
    public interface IRecommendationContext
    {
        string? GetFavouriteAuthorId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList);
        string? GetFavouriteGenreId(IEnumerable<string> genreIdList);
    }
}
