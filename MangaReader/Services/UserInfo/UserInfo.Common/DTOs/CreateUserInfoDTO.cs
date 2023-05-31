using System;
namespace UserInfo.API.DTOs
{
	public class CreateUserInfoDTO
	{
		public string userId { get; set; } = string.Empty;

        public CreateUserInfoDTO(string userId)
        {
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
        }
    }
}