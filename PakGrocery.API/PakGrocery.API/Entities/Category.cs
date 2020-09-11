using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Entities
{
    public class Category : EntityBase
    {
        public string CatName { get; set; }
        public string ParentId { get; set; }
    }
}
