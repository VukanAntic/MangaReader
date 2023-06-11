using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.DTOs.Page;
using MangaCatalog.Common.Entities;

namespace MangaCatalog.Common.Repositories.Interfaces
{
    public interface IChapterRepository
    {
        Task<IEnumerable<ChapterDTO>> GetChaptersByMangaId(string mangaId);
        Task<ChapterDTO> GetChapterById(string chapterId);
        Task<IEnumerable<PageDTO>> GetPagesForChapterId(string chapterId);

    }
}
