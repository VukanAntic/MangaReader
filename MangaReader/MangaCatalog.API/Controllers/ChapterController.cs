using MangaCatalog.API.Entities;
using MangaCatalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MangaCatalog.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ChapterController : ControllerBase
    {

        private readonly IChapterRepository _repository;

        public ChapterController(IChapterRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [Route("[action]/{mangaId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Chapter>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Chapter>>> getAllChaptersForMangaId(string mangaId)
        {
            var chapters = await _repository.GetChaptersByMangaId(mangaId);
            return Ok(chapters);

        }


        [Route("[action]/{chapterId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Chapter>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Chapter>>> getPagesForChapterId(string chapterId)
        {
            var pages = await _repository.getPagesForChapterId(chapterId);
            return Ok(pages);

        }


    }
}
