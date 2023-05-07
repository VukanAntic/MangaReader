using MangaCatalog.API.DTOs;

namespace MangaCatalog.API.Repositories.Interfaces
{
    public interface IMangaRepository
    {
        Task<IEnumerable<MangaDTO>> GetAllMangas();
        Task<DescriptiveMangaDTO?> GetMangaById(string id);

        Task<IEnumerable<MangaDTO>> GetMangasByGenre(string genreId);

        Task<IEnumerable<MangaDTO>> GetMangasByAuthor(string authorId);

        Task<IEnumerable<MangaDTO>> GetMangasBySearch(string queryString);
    }
}
