namespace UserInfo.Common.Entities
{
    public class UserInformation
    {
        public string ID { get; set; }
        public ReadingJournal ReadingJournal { get; set; } = new ReadingJournal();

        public UserInformation()
        {
        }

        public UserInformation(string iD)
        {
            ID = iD ?? throw new ArgumentNullException(nameof(iD));
        }
    }
}
