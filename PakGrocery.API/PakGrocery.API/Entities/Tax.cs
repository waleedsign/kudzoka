using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Entities
{
    public class Tax : EntityBase
    {
        public string TaxName { get; set; }
        public decimal PercentageOfTax { get; set; }
    }
}
