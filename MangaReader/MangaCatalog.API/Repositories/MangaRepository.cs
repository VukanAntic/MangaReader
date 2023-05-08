﻿using AutoMapper;
using Dapper;
using MangaCatalog.API.Data;
using MangaCatalog.API.DTOs;
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

            var mangas = await connection.QueryAsync<Manga>("SELECT * FROM manga");

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<MangaDTO?> GetMangaById(string id)
        {
            using var connection = _context.GetConnection();

            var manga = await connection.QueryFirstOrDefaultAsync<Manga>(
                "SELECT * FROM manga WHERE ID = @MangaId",
                new { MangaId = id });

            return _mapper.Map<MangaDTO>(manga);
        }


        // Could be possible that we might want to search via author name or genre name, so this might need changes
        public async Task<IEnumerable<MangaDTO>> GetMangasByAuthorId(string authorId)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT * FROM manga WHERE author_id = @AuthorId",
                new { AuthorId = authorId });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasByGenreName(string genreName)
        {
            using var connection = _context.GetConnection();

            var mangas = await connection.QueryAsync<Manga>(
                "SELECT * FROM manga m WHERE id in " +
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
                "SELECT * FROM manga WHERE ID in " +
                "(SELECT id_manga FROM manga_genre WHERE id_genre = @Genre)",
                new { Genre = genreId });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }

        public async Task<IEnumerable<MangaDTO>> GetMangasBySearch(string queryString)
        {
            using var connection = _context.GetConnection();

            // TODO: FIX this search implementation
            var mangas = await connection.QueryAsync<Manga>(
                "SELECT * FROM manga WHERE LOWER(title) LIKE CONCAT('%',@Query,'%')",
                new { Query = queryString });

            return _mapper.Map<IEnumerable<MangaDTO>>(mangas);
        }
    }
}
