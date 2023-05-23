using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class UpdateAllReadMangaEvent : IntegrationBaseEvent
    {
        public string userId { get; set; }
        public string mangaId { get; set; }

        public UpdateAllReadMangaEvent(string userId, string mangaId)
        {
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
            this.mangaId = mangaId ?? throw new ArgumentNullException(nameof(mangaId));
        }
    }
}
