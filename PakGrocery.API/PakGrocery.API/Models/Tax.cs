using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class Tax : ModelBase
    {
        public string TaxName { get; set; }
        public decimal PercentageOfTax { get; set; }
    }
}
