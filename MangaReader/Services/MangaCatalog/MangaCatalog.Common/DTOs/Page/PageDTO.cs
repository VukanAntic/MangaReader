using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCatalog.Common.DTOs.Page
{
    public class PageDTO
    {
        public string ChapterId { get; set; }
        public int PageNumber { get; set; }
        public string ImageLink { get; set; }
    }
}
