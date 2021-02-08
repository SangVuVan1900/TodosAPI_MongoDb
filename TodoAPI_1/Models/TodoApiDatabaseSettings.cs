using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAPI_1.Models 
{ 
    public class TodoApiDatabaseSettings : ITodoApiDatabaseSettings
    { 
        public string TodosCollectionName { get; set; }  
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
     
    public interface ITodoApiDatabaseSettings 
    {  
        string TodosCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
