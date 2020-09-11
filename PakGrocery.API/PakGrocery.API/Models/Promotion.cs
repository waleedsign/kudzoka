using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class Promotion : ModelBase
    {
        public string PromotionName { get; set; }
        public string Description { get; set; }
        public string StoreId { get; set; }
    }
}
