using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class Category:ModelBase
    {
        public string CatName { get; set; }
        public string ParentId { get; set; }
    }
}
