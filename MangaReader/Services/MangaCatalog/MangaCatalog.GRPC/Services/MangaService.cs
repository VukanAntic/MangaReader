using AutoMapper;
using Grpc.Core;
using MangaCatalog.Common.Repositories.Interfaces;
using MangaCatalog.GRPC.Protos;
using Microsoft.Extensions.Logging;

namespace MangaCatalog.GRPC.Services
{
    public class MangaService : MangaProtoService.MangaProtoServiceBase
    {
        private readonly IMangaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<MangaService> _logger;

        public MangaService(IMangaRepository repository, IMapper mapper, ILogger<MangaService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<GetMangaResponse> GetManga(GetMangaRequest request, ServerCallContext context)
        {
            var manga = await _repository.GetMangaById(request.MangaId)
                                ?? throw new RpcException(new Status(StatusCode.NotFound, $"Manga with ID = {request.MangaId} is not found"));

            _logger.LogInformation("Manga with ID: {mangaId} is retrieved", request.MangaId);
            
            var response = new GetMangaResponse();
            response.Manga = _mapper.Map<Manga>(manga);

            return response;
        }

        public override async Task<GetMangasResponse> GetMangas(GetMangasRequest request, ServerCallContext context)
        {
            var mangas = await _repository.GetMangasByIds(request.MangaIds)
                                ?? throw new RpcException(new Status(StatusCode.NotFound, $"One or more mangas from the following list of IDs were not found: {request.MangaIds}"));
            _logger.LogInformation("Mangas with following IDs have been retrieved: {mangaIds}", request.MangaIds);

            var response = new GetMangasResponse();
            response.Mangas.AddRange(_mapper.Map<IEnumerable<Manga>>(mangas));

            return response;
        }
    }
}
