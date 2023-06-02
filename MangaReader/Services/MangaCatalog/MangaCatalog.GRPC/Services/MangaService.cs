using AutoMapper;
using Grpc.Core;
using MangaCatalog.Common.Repositories.Interfaces;
using MangaCatalog.GRPC.Protos;

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

        public override async Task<GetMangaGenresResponse> GetMangaGenres(GetMangaGrenresRequest request, ServerCallContext context)
        {
            var genres = await _repository.GetAllGenresOfMangaById(request.MangaId)
                                ?? throw new RpcException(new Status(StatusCode.NotFound, $"Genres for manga with ID = {request.MangaId} not found"));

            var response = new GetMangaGenresResponse();
            response.Genres.AddRange(_mapper.Map<IEnumerable<GetMangaGenresResponse.Types.Genre>>(genres));

            return response;
        }

        public override async Task<GetMangasByAuthorIdResponse> GetMangasByAuthorId(GetMangasByAuthorIdRequest request, ServerCallContext context)
        {
            var mangas = await _repository.GetMangasByAuthorId(request.AuthorId)
                                ?? throw new RpcException(new Status(StatusCode.NotFound, $"Mangas for author with ID = {request.AuthorId} not found"));
            var response = new GetMangasByAuthorIdResponse();
            response.Mangas.AddRange(_mapper.Map<IEnumerable<Manga>>(mangas));

            return response;
        }

        public override async Task<GetMangasByGenreIdResponse> GetMangasByGenreId(GetMangasByGenreIdRequest request, ServerCallContext context)
        {
            var mangas = await _repository.GetMangasByGenreId(request.GenreId)
                                ?? throw new RpcException(new Status(StatusCode.NotFound, $"Mangas for genre with ID = {request.GenreId} not found"));
            var response = new GetMangasByGenreIdResponse();
            response.Mangas.AddRange(_mapper.Map<IEnumerable<Manga>>(mangas));

            return response;
        }
    }
}
