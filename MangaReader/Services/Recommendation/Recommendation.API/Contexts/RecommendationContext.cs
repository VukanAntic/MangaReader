using MangaCatalog.Common.DTOs.Manga;

namespace Recommendation.API.Contexts
{
    public class RecommendationContext : IRecommendationContext
    {
        public string? GetFavouriteAuthorId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList)
        {
            var authorCount = new Dictionary<string, int>();

            foreach(var manga in readList)
            {
                if (authorCount.ContainsKey(manga.AuthorId))
                {
                    authorCount[manga.AuthorId]++;
                } else
                {
                    authorCount[manga.AuthorId] = 1;
                }
            }
            foreach (var manga in wishList)
            {
                if (authorCount.ContainsKey(manga.AuthorId))
                {
                    authorCount[manga.AuthorId]++;
                }
                else
                {
                    authorCount[manga.AuthorId] = 1;
                }
            }

            string favouriteAuthorId = null;
            int authorNumber = 0;

            foreach(var kvp in authorCount)
            {
                if(kvp.Value >  authorNumber)
                {
                    favouriteAuthorId = kvp.Key;
                    authorNumber = kvp.Value;
                }
            }

            return favouriteAuthorId;
        }

        public string? GetFavouriteGenreId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList)
        {
            throw new NotImplementedException();
        }
    }
}
