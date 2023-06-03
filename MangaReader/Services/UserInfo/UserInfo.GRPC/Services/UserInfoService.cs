using AutoMapper;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInfo.API.Repository;
using UserInfo.GRPC.Protos;

namespace UserInfo.GRPC.Services
{
    public class UserInfoService : UserInfoProtoService.UserInfoProtoServiceBase
    {

        private readonly IUserInformationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserInfoService> _logger;

        public UserInfoService(IUserInformationRepository repository, IMapper mapper, ILogger<UserInfoService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async override Task<GetUserInfoResponse> GetUserInfo(GetUserInfoRequest request, ServerCallContext context)
        {
            var userInfo = await _repository.GetUserInfo(request.Id)
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"UserInfo with Id = {request.Id} is not found"));

            _logger.LogInformation("UserInfo with ID: {mangaId} is retrieved", request.Id);

            var response = new GetUserInfoResponse();
            response.ReadingJournal.LastReadMangaID = userInfo.ReadingJournal.LastReadMangaID;
            response.ReadingJournal.AllReadMangaIDs.AddRange(userInfo.ReadingJournal.AllReadMangaIDs);
            response.ReadingJournal.WishList.AddRange(userInfo.ReadingJournal.WishList);
            return response;
        }
    }
}
