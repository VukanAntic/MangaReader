namespace UserInfo.API.Entities
{
    public class ReadingJournal
    {
        public string LastReadMangaID { get; set; } = string.Empty;
        public List<string> AllReadMangaIDs { get; set; } = new List<string>();
        public List<string> WishList { get; set; } = new List<string>();
    }
}
