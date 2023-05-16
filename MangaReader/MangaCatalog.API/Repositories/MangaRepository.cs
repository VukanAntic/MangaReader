using AutoMapper;
using Dapper;
using MangaCatalog.API.Data;
using MangaCatalog.API.DTOs.Author;
using MangaCatalog.API.DTOs.Chapter;
using MangaCatalog.API.DTOs.Manga;
using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories.Interfaces;

namespace MangaCatalog.API.Repositories
{
    public class MangaRepository : IMangaRepository
    {
        private readonly IMangaCatalogContext _context;
        private readonly IMapper _mapper;

        public MangaRepository(IMangaCatalogContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<IEnumerable<MangaDTO>> GetAllMangas()
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga");

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<MangaDTO?> GetMangaById(string id)
        {
            using var connection = _context.GetConnection();

            var manga = await connection.QueryFirstOrDefaultAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga " +
                "WHERE ID = @MangaId",
                new { MangaId = id });

            return _mapper.Map<MangaDTO>(manga);
        }


        // Could be possible that we might want to search via author name or genre name, so this might need changes
        public async Task<IEnumerable<MangaDTO>> GetMangasByAuthorId(string authorId)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga " +
                "WHERE author_id = @AuthorId",
                new { AuthorId = authorId });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasByGenreName(string genreName)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga m " +
                "WHERE id in " +
                "(SELECT m_g.id_manga " +
                "FROM manga_genre m_g " +
                "JOIN genre g ON m_g.id_genre = g.id " +
                "WHERE m.id = m_g.id_manga AND g.name = @GenreName) ",
                new { GenreName = genreName });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasByGenreId(string genreId)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga " +
                "WHERE ID in " +
                "(SELECT id_manga FROM manga_genre WHERE id_genre = @Genre)",
                new { Genre = genreId });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasBySearch(string queryString)
        {
            using var connection = _context.GetConnection();

            // TODO: FIX this search implementation
            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga " +
                "WHERE LOWER(title) LIKE CONCAT('%',LOWER(@Query),'%')",
                new { Query = queryString });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
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

        public async Task<AuthorDTO> GetAuthorInfoById(string authorId)
        {
            using var connection = _context.GetConnection();

            var authorInfo = await connection.QueryFirstOrDefaultAsync<Author>(
                "SELECT * " +
                "FROM author " +
                "WHERE id = @AuthorId",
                new { AuthorId = authorId });

            return _mapper.Map<AuthorDTO>(authorInfo);
        }
    }
}
