using UserInfo.API.Entities;

namespace UserInfo.API.DTOs
{
    public class UserInfoDTO
    {
        public string ID { get; set; }
        public ReadingJournal ReadingJournal { get; set; } = new ReadingJournal();

    }
}
