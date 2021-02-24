using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI_1.Models
{
    public enum SortType 
    {
        [Description("Title")]
        Title,
        [Description("CreatedDate")]
        CreatedDate,
        [Description("UpdatedDate")] 
        UpdatedDate
        //Title, CreatedDate, UpdatedDate
    }
}
