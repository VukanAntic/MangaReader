using MangaCatalog.API.DTOs.Chapter;

namespace MangaCatalog.API.Repositories.Interfaces
{
    public interface IChapterRepository
    {
        Task<IEnumerable<ChapterDTO>> GetChaptersByMangaId(string mangaId);
    }
}
