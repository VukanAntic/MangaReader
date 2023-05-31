using MangaCatalog.Common.DTOs.Manga;
using MangaCatalog.Common.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Contexts;
using Recommendation.API.DTOs;

namespace Recommendation.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationContext _context;

        public RecommendationController(IRecommendationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(RecommendationPageDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<RecommendationPageDTO>> GetRecommendationPage(string userId)
        {
            // Current plan: Use controler to create and fill a RecommendationPageDTO object with information using awailable methods from the _context object.

            // Controler contacts UserInfo, gets ReadList and WishList and passes them to the _context methods to get information about the users 
            // favourite author and genre, which the controller then uses to contact Manga service to get mangas from that author/genre

            //temp
            IEnumerable<MangaDTO> readList = null;
            IEnumerable<MangaDTO> wishList = null;

            var favouriteAuthorId = await _context.GetFavouriteAuthorId(readList,wishList);
            var facouriteGenreId = await _context.GetFavouriteGenreId(readList, wishList);

            var recommendationPage = new RecommendationPageDTO();

            return Ok(recommendationPage);
        }
    }
}
