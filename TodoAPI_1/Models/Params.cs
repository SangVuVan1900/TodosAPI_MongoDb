using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI_1.Models
{
    public class Params
    {
        public bool Done { get; set; }
        public bool SortByAscending { get; set; }
        public string Title { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;
    }
}
