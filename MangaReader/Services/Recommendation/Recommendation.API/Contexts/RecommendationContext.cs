using Recommendation.API.DTOs;

namespace Recommendation.API.Contexts
{
    public class RecommendationContext : IRecommendationContext
    {
        public Task<RecommendationPageDTO> GetRecommendedContent(string userId) {
            throw new NotImplementedException();
        }
    }
}
