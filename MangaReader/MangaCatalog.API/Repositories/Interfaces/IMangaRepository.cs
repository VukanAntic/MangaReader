using MangaCatalog.API.DTOs.Author;
using MangaCatalog.API.DTOs.AuthorPageResponse;
using MangaCatalog.API.DTOs.Genre;
using MangaCatalog.API.DTOs.Manga;

namespace MangaCatalog.API.Repositories.Interfaces
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

        Task<AuthorPageResponseDTO?> GetAuthorPageResponseByAuthorId(string authorId);

        Task<IEnumerable<GenreDTO>> GetAllGenresOfMangaById(string mangaId);

        Task<GenreDTO?> GetGenreById(string genreId);
    }
}
