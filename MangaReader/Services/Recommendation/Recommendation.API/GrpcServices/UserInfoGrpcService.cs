using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserInfo.GRPC.Protos;

namespace Recommendation.API.GrpcServices
{
    public class UserInfoGrpcService
    {
        private readonly UserInfoProtoService.UserInfoProtoServiceClient _userInfoProtoServiceClient;

        public UserInfoGrpcService(UserInfoProtoService.UserInfoProtoServiceClient userInfoProtoServiceClient)
        {
            _userInfoProtoServiceClient = userInfoProtoServiceClient ?? throw new ArgumentNullException(nameof(userInfoProtoServiceClient));
        }

        public async Task<GetUserInfoResponse> GetUserInfo(string userId)
        {
            var request = new GetUserInfoRequest();
            request.Id = userId;
            return await _userInfoProtoServiceClient.GetUserInfoAsync(request);
        }
    }
}
