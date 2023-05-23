using MangaCatalog.Common.DTOs.Author;
using MangaCatalog.Common.DTOs.Manga;

namespace MangaCatalog.Common.DTOs.AuthorPageResponse
{
    public class BaseAuthorPageResponseDTO
    {
        public AuthorDTO AuthorInfo { get; set; }
        public IEnumerable<MangaDTO> MangaList { get; set; }
    }
}
