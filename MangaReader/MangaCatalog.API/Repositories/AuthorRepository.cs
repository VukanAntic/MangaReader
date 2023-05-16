using AutoMapper;
using Dapper;
using MangaCatalog.API.Data;
using MangaCatalog.API.DTOs.Author;
using MangaCatalog.API.DTOs.Manga;
using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories.Interfaces;

namespace MangaCatalog.API.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IMangaCatalogContext _context;
        private readonly IMapper _mapper;

        public AuthorRepository(IMangaCatalogContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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
