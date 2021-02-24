using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TodoAPI_1.Models
{ 
    //[DataContract]  
    public enum SortType
    {
        [EnumMember(Value = "Title")]
        Title,
        [EnumMember(Value = "Created Date")]
        CreatedDate,
        [EnumMember(Value = "Updated Date")]
        UpdatedDate
    }
}
