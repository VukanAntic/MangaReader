using MangaCatalog.GRPC.Protos;

namespace Recommendation.API.GrpcServices
{
    public class MangaGrpcService
    {
        private readonly MangaProtoService.MangaProtoServiceClient _mangaProtoServiceClient;

        public MangaGrpcService(MangaProtoService.MangaProtoServiceClient mangaProtoServiceClient)
        {
            _mangaProtoServiceClient = mangaProtoServiceClient ?? throw new ArgumentNullException(nameof(mangaProtoServiceClient));
        }

        public async Task<GetMangaResponse> GetManga(string mangaId)
        {
            var mangaRequest = new GetMangaRequest();
            mangaRequest.MangaId = mangaId;
            return await _mangaProtoServiceClient.GetMangaAsync(mangaRequest);
        }

        public async Task<GetMangasResponse> GetMangas(IEnumerable<string> mangaIds)
        {
            var mangasRequest = new GetMangasRequest();
            mangasRequest.MangaIds.Add(mangaIds);
            return await _mangaProtoServiceClient.GetMangasAsync(mangasRequest);
        }
    }
}
