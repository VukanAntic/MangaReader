using UserInfo.Common.Entities;

namespace UserInfo.Common.DTOs
{
    public class UserInfoDTO
    {
        public string ID { get; set; }
        public ReadingJournal ReadingJournal { get; set; } = new ReadingJournal();

    }
}
