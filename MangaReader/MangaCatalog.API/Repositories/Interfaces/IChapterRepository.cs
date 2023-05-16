using MangaCatalog.API.DTOs.Chapter;
using MangaCatalog.API.Entities;

namespace MangaCatalog.API.Repositories.Interfaces
{
    public interface IChapterRepository
    {
        Task<IEnumerable<ChapterDTO>> GetChaptersByMangaId(string mangaId);
        Task<IEnumerable<Page>> GetPagesForChapterId(string chapterId);

    }
}
