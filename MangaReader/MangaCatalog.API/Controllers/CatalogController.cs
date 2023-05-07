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
        public async Task<ActionResult<IEnumerable<Manga>>> getProducts()
        {
            var allMangas = await _repository.GetAllMangas();
            return Ok(allMangas);
        }

    }
}