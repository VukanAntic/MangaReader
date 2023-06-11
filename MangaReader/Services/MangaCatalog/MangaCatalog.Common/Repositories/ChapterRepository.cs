using AutoMapper;
using Dapper;
using MangaCatalog.Common.Data;
using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.DTOs.Page;
using MangaCatalog.Common.Entities;
using MangaCatalog.Common.Repositories.Interfaces;

namespace MangaCatalog.Common.Repositories
{
    public class ChapterRepository : IChapterRepository
    {
        private readonly IMangaCatalogContext _context;
        private readonly IMapper _mapper;


        public ChapterRepository(IMangaCatalogContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<ChapterDTO>> GetChaptersByMangaId(string mangaId)
        {
            using var connection = _context.GetConnection();

            var chapters = await connection.QueryAsync<Chapter>(
                "SELECT id, id_manga as idManga, chapter_number as chapterNumber, title " +
                "FROM chapter " +
                "WHERE chapter.id_manga = @MangaId " +
                "ORDER BY CAST(chapter.chapter_number as float) ASC, chapter.chapter_number ASC;",
                new { MangaId = mangaId });

            return _mapper.Map<IEnumerable<ChapterDTO>>(chapters);
        }

        public async Task<IEnumerable<PageDTO>> GetPagesForChapterId(string chapterId)
        {
            using var connection = _context.GetConnection();

            Console.WriteLine(chapterId);
            var chapters = await connection.QueryAsync<PageDTO>(
                "SELECT chapter_id as ChapterId, page_number as pageNumber, image_link as imageLink " +
                "FROM page " +
                "WHERE chapter_id = @ChapterId " +
                "ORDER BY page_number ASC;",
                new { ChapterId = chapterId });

            return chapters;
        }

        public async Task<ChapterDTO> GetChapterById(string chapterId)
        {
            using var connection = _context.GetConnection();

            var chapter = await connection.QueryFirstOrDefaultAsync<Chapter>(
                "SELECT id, id_manga as idManga, chapter_number as chapterNumber, title " +
                "FROM chapter " +
                "WHERE chapter.id = @ChapterId;",
                new { ChapterId = chapterId });

            return _mapper.Map<ChapterDTO>(chapter);
        }
    }
}
