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
            var recommendationPage = await _context.GetRecommendedContent(userId);
            return Ok(recommendationPage);
        }
    }
}
