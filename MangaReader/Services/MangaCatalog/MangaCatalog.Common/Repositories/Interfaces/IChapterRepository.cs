using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.Entities;

namespace MangaCatalog.Common.Repositories.Interfaces
{
    public interface IChapterRepository
    {
        Task<IEnumerable<ChapterDTO>> GetChaptersByMangaId(string mangaId);
        Task<IEnumerable<Page>> GetPagesForChapterId(string chapterId);

    }
}
