using System;
namespace UserInfo.Common.DTOs
{
	public class UpdateUserInfoDTO // moze da posluzi za sva 3 razlicita zahteva za update
	{
		public string userId { get; set; } = string.Empty;
		public string mangaId { get; set; } = string.Empty;

        public UpdateUserInfoDTO(string userId, string mangaId)
        {
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
            this.mangaId = mangaId ?? throw new ArgumentNullException(nameof(mangaId));
        }
    }
}

