namespace UserInfo.API.Entities
{
    public class ReadingJournal
    {
        public string LastReadMangaID { get; set; }
        public List<string> AllReadMangaIDs { get; set; }
   		public List<string> WishList { get; set; }

    }
}
