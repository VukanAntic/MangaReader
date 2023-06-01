using AutoMapper;
using Dapper;
using MangaCatalog.Common.Data;
using MangaCatalog.Common.DTOs.Author;
using MangaCatalog.Common.DTOs.Chapter;
using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.Common.Entities;
using MangaCatalog.Common.Repositories.Interfaces;

namespace MangaCatalog.Common.Repositories
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
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
                "FROM manga");

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<MangaDTO?> GetMangaById(string id)
        {
            using var connection = _context.GetConnection();

            var manga = await connection.QueryFirstOrDefaultAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
                "FROM manga " +
                "WHERE ID = @MangaId",
                new { MangaId = id });

            return _mapper.Map<MangaDTO>(manga);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasByIds(IEnumerable<string> ids)
        {
            using var connection = _context.GetConnection();

            var mangas = new List<MangaDTO>();

            foreach (var id in ids)
            {
                var manga = await connection.QueryFirstOrDefaultAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
                "FROM manga " +
                "WHERE ID = @MangaId",
                new { MangaId = id });

                mangas.Add(_mapper.Map<MangaDTO>(manga));
            }
                    
            return mangas;
        }


        // Could be possible that we might want to search via author name or genre name, so this might need changes

        // Probably not useful
        public async Task<IEnumerable<MangaDTO>> GetMangasByAuthorId(string authorId)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
                "FROM manga " +
                "WHERE author_id = @AuthorId",
                new { AuthorId = authorId });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasByGenreName(string genreName)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
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
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
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
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId, number_of_ratings as NumberOfRatings, sum_of_ratings as SumOfRatings " +
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


        // Probably not useful
        public async Task<AuthorDTO?> GetAuthorInfoById(string authorId)
        {
            using var connection = _context.GetConnection();

            var authorInfo = await connection.QueryFirstOrDefaultAsync<Author>(
                "SELECT * " +
                "FROM author " +
                "WHERE id = @AuthorId",
                new { AuthorId = authorId });

            return _mapper.Map<AuthorDTO>(authorInfo);
        }



        // The code below was most likely a mistake and is left just in case
        /*public async Task<AuthorPageResponseDTO?> GetAuthorPageResponseByAuthorId(string authorId) 
        {
            using var connection = _context.GetConnection();

            var authorInfo = await connection.QueryFirstOrDefaultAsync<Author>(
                "SELECT * " +
                "FROM author " +
                "WHERE id = @AuthorId",
                new { AuthorId = authorId });

            var mangaList = await connection.QueryAsync<Manga>(
                "SELECT id, title, description, status, cover_art as coverArt, content_rating as contentRating, author_id as authorId " +
                "FROM manga " +
                "WHERE author_id = @AuthorId",
                new { AuthorId = authorId });

            var authorPageResponse = new AuthorPageResponse{
                AuthorInfo = authorInfo,
                MangaList = mangaList
            };

            return _mapper.Map<AuthorPageResponseDTO>(authorPageResponse);
        }*/

        public async Task<IEnumerable<GenreDTO>> GetAllGenresOfMangaById(string mangaId) { 
            using var connection = _context.GetConnection();

            var genres = await connection.QueryAsync<Genre>(
                "SELECT * " +
                "FROM genre g JOIN manga_genre m_g ON g.id = m_g.id_genre " +
                "WHERE m_g.id_manga = @MangaId", new { MangaId = mangaId});

            return _mapper.Map<IEnumerable<GenreDTO>>(genres);
        }
        public async Task<GenreDTO?> GetGenreById(string genreId) {
            using var connection = _context.GetConnection();

            var genre = await connection.QueryFirstOrDefaultAsync<Genre>(
                "SELECT * " +
                "FROM genre " +
                "WHERE id = @GenreId", new { GenreId = genreId });

            return _mapper.Map<GenreDTO>(genre);
        }

        public async Task<bool> AddMangaRating(string mangaId, int rating) {
            using var connection = _context.GetConnection();

            var affected = await connection.ExecuteAsync(
                "UPDATE Manga " +
                "SET number_of_ratings = number_of_ratings + 1,  sum_of_ratings = sum_of_ratings + @Rating " +
                "WHERE id = @Id", new { Rating = rating, Id = mangaId });

            return affected != 0;
        }

    }
}
