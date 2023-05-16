using MangaCatalog.API.DTOs.Author;
using MangaCatalog.API.DTOs.Manga;

namespace MangaCatalog.API.DTOs.AuthorPageResponse
{
    public class BaseAuthorPageResponseDTO
    {
        public AuthorDTO AuthorInfo { get; set; }
        public IEnumerable<MangaDTO> MangaList { get; set; }
    }
}
