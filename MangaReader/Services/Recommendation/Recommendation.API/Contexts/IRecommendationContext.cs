using Recommendation.API.DTOs;

namespace Recommendation.API.Contexts
{
    public interface IRecommendationContext
    {
        Task<RecommendationPageDTO> GetRecommendedContent(string userId);
    }
}
