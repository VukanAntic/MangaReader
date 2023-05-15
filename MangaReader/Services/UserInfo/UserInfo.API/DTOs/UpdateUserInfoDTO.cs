using System;
namespace UserInfo.API.DTOs
{
	public class UpdateUserInfoDTO // moze da posluzi za sva 3 razlicita zahteva za update
	{
		public string userId { get; set; } = string.Empty;
		public string mangaId { get; set; } = string.Empty;
	}
}

