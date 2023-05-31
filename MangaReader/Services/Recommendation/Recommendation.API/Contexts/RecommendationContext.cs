using MangaCatalog.Common.DTOs.Manga;

namespace Recommendation.API.Contexts
{
    public class RecommendationContext : IRecommendationContext
    {
        public Task<string> GetFavouriteAuthorId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetFavouriteGenreId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList)
        {
            throw new NotImplementedException();
        }
    }
}
