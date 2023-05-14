using AutoMapper;
using Dapper;
using MangaCatalog.API.Data;
using MangaCatalog.API.DTOs.Chapter;
using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories.Interfaces;

namespace MangaCatalog.API.Repositories
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
    }
}
