using MangaCatalog.Common.DTOs.Author;
using MangaCatalog.Common.DTOs.AuthorPageResponse;
using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;

namespace MangaCatalog.Common.Repositories.Interfaces
{
    public interface IMangaRepository
    {
        Task<IEnumerable<MangaDTO>> GetAllMangas();

        Task<MangaDTO?> GetMangaById(string id);

        Task<IEnumerable<MangaDTO>> GetMangasByGenreId(string genreId);

        Task<IEnumerable<MangaDTO>> GetMangasByGenreName(string genreName);

        Task<IEnumerable<MangaDTO>> GetMangasByAuthorId(string authorId);

        Task<IEnumerable<MangaDTO>> GetMangasBySearch(string queryString);

        Task<AuthorDTO?> GetAuthorInfoById(string authorId);

        // The code below was most likely a mistake and is left just in case
        //Task<AuthorPageResponseDTO?> GetAuthorPageResponseByAuthorId(string authorId);

        Task<IEnumerable<GenreDTO>> GetAllGenresOfMangaById(string mangaId);

        Task<GenreDTO?> GetGenreById(string genreId);

        Task<bool> AddMangaRating(string mangaId, int rating);    
    
    }
}
