using MangaCatalog.API.DTOs;

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
    }
}
