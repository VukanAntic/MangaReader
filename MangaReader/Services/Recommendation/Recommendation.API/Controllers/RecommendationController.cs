using AutoMapper;
using MangaCatalog.Common.DTOs.Manga;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recommendation.API.Contexts;
using Recommendation.API.DTOs;
using Recommendation.API.GrpcServices;

namespace Recommendation.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationContext _context;
        private readonly MangaGrpcService _mangaGrpcService;
        private readonly IMapper _mapper;

        public RecommendationController(IRecommendationContext context, MangaGrpcService mangaGrpcService, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mangaGrpcService = mangaGrpcService ?? throw new ArgumentNullException(nameof(mangaGrpcService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(RecommendationPageDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<RecommendationPageDTO>> GetRecommendationPage(string userId)
        {
            // Current plan: Use controler to create and fill a RecommendationPageDTO object with information using awailable methods from the _context object.

            // Controler contacts UserInfo, gets ReadList and WishList and passes them to the _context methods to get information about the users 
            // favourite author and genre, which the controller then uses to contact Manga service to get mangas from that author/genre

            // temp
            IEnumerable<string> readListIds = null; // both of these lists will come from GRPC communication with UserInfo.API
            IEnumerable<string> wishListIds = null;

            var readMangaResponse = await _mangaGrpcService.GetMangas(readListIds);
            var wishMangaResponse = await _mangaGrpcService.GetMangas(wishListIds);

            // experimental, might not work, might need to search through mangas one by one, or just use _mangaGrpcService.GetManga to get mangas one by one and map them
            IEnumerable<MangaDTO> readListMangas = _mapper.Map<IEnumerable<MangaDTO>>(readMangaResponse.Mangas);
            IEnumerable<MangaDTO> wishListMangas = _mapper.Map<IEnumerable<MangaDTO>>(wishMangaResponse.Mangas);

            var favouriteAuthorId = await _context.GetFavouriteAuthorId(readListMangas, wishListMangas);
            var facouriteGenreId = await _context.GetFavouriteGenreId(readListMangas, wishListMangas);

            var recommendationPage = new RecommendationPageDTO();

            return Ok(recommendationPage);
        }
    }
}
