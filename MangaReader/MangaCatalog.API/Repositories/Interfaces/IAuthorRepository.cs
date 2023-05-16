using MangaCatalog.API.DTOs.Author;
using MangaCatalog.API.Entities;

namespace MangaCatalog.API.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<AuthorDTO> GetAuthorInfoById(string authorId);
    }
}
