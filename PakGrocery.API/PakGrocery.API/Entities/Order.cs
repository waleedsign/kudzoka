using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Entities
{
    public class Order : EntityBase
    {
        public string BillingAddress { get; set; }
        public decimal Discount { get; set; }
    }
}
