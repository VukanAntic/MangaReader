using MangaCatalog.API.DTOs;
using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories.Interfaces;
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
        [ProducesResponseType(typeof(IEnumerable<Manga>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangas()
        {
            var allMangas = await _repository.GetAllMangas();
            return Ok(allMangas);
        }

        [HttpGet("{id}", Name = "GetManga")]
        [ProducesResponseType(typeof(Manga), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Manga), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Manga>> GetMangaById(string id)
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
        [ProducesResponseType(typeof(IEnumerable<Manga>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangasByGenreName(string genreName)
        {
            var mangas = await _repository.GetMangasByGenreName(genreName);
            return Ok(mangas);
        }

        [Route("[action]/{genreId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Manga>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangasByGenreId(string genreId)
        {
            var mangas = await _repository.GetMangasByGenreId(genreId);
            return Ok(mangas);
        }


        [Route("[action]/{authorId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Manga>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangasByAuthorId(string authorId)
        {
            var mangas = await _repository.GetMangasByAuthorId(authorId);
            return Ok(mangas);
        }


        [Route("[action]/{searchRequest}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Manga>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Manga>>> GetMangasBySearch(string searchRequest)
        {
            var mangas = await _repository.GetMangasBySearch(searchRequest);
            return Ok(mangas);
        }

    }
}