using MangaCatalog.Common.DTOs.Manga;
using System.Linq;

namespace Recommendation.API.Contexts
{
    public class RecommendationContext : IRecommendationContext
    {
        public RecommendationContext() { }
        public string GetFavouriteAuthorId(IEnumerable<MangaDTO> readList, IEnumerable<MangaDTO> wishList)
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

            string favouriteAuthorId = readList.First().AuthorId;
            int authorNumber = 0;

            foreach(var kvp in authorCount)
            {
                if(kvp.Value > authorNumber)
                {
                    favouriteAuthorId = kvp.Key;
                    authorNumber = kvp.Value;
                }
            }

            return favouriteAuthorId;
        }

        public string GetFavouriteGenreId(IEnumerable<string> genreIdList)
        {
            var genreCount = new Dictionary<string, int>();

            foreach(var genre in genreIdList)
            {
                if (genreCount.ContainsKey(genre))
                {
                    genreCount[genre]++;
                }
                else
                {
                    genreCount[genre] = 1;
                }
            }

            string favouriteGenre = genreIdList.First();
            int genreNumber = 0;

            foreach (var kvp in genreCount)
            {
                if (kvp.Value > genreNumber)
                {
                    favouriteGenre = kvp.Key;
                    genreNumber = kvp.Value;
                }
            }

            return favouriteGenre;
        }
    }
}
