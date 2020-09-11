using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class Order : ModelBase
    {
        public string BillingAddress { get; set; }
        public decimal Discount { get; set; }
    }
}
