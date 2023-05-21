using MangaCatalog.Common.DTOs.Author;
using MangaCatalog.Common.DTOs.Genre;
using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.Common.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MangaCatalog.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {

        private readonly IMangaRepository _repository;

        public CatalogController(IMangaRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MangaDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MangaDTO>>> GetMangas()
        {
            var allMangas = await _repository.GetAllMangas();
            return Ok(allMangas);
        }

        [HttpGet("{id}", Name = "GetManga")]
        [ProducesResponseType(typeof(MangaDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MangaDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MangaDTO>> GetMangaById(string id)
        {
            var manga = await _repository.GetMangaById(id);
            if (manga is null)
            {
                return NotFound(null);
            }
            return Ok(manga);
        }

        [Route("[action]/{genreName}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MangaDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MangaDTO>>> GetMangasByGenreName(string genreName)
        {
            var mangas = await _repository.GetMangasByGenreName(genreName);
            return Ok(mangas);
        }

        [Route("[action]/{genreId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MangaDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MangaDTO>>> GetMangasByGenreId(string genreId)
        {
            var mangas = await _repository.GetMangasByGenreId(genreId);
            return Ok(mangas);
        }


        [Route("[action]/{authorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MangaDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MangaDTO>>> GetMangasByAuthorId(string authorId)
        {
            var mangas = await _repository.GetMangasByAuthorId(authorId);
            return Ok(mangas);
        }

        [Route("[action]/{authorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthorDTO>> GetAuthorInfoById(string authorId)
        {
            var authorInfo = await _repository.GetAuthorInfoById(authorId);
            if (authorInfo is null)
            {
                return NotFound(null);
            }

            return Ok(authorInfo);
        }

        // The code below was most likely a mistake and is left just in case

        /*[Route("[action]/{authorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MangaDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MangaDTO>>> GetAuthorPageResponse(string authorId)
        {
            var authorPageResponse = await _repository.GetAuthorPageResponseByAuthorId(authorId);
            if(authorPageResponse is null)
            {
                return NotFound(null);
            }

            return Ok(authorPageResponse);
        }*/


        [Route("[action]/{searchRequest}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MangaDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MangaDTO>>> GetMangasBySearch(string searchRequest)
        {
            var mangas = await _repository.GetMangasBySearch(searchRequest);
            return Ok(mangas);
        }

        [Route("[action]/{mangaId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAllGenresOfMangaById(string mangaId)
        {
            var mangas = await _repository.GetAllGenresOfMangaById(mangaId);
            return Ok(mangas);
        }

        [Route("[action]/{genreId}")]
        [HttpGet]
        [ProducesResponseType(typeof(GenreDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<GenreDTO>> GetGenreById(string genreId)
        {
            var mangas = await _repository.GetGenreById(genreId);
            return Ok(mangas);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMangaRating([FromBody] AddMangaRatingDTO addMangaRatingDTO)
        {

            bool isUpdated = await _repository.AddMangaRating(addMangaRatingDTO.Id, addMangaRatingDTO.Rating);
            if (isUpdated)
                return Accepted();
            return BadRequest();
        }
    }
}