using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Entities
{
    public class Product: EntityBase
    {
        public string ProductName{ get; set; }
        public decimal WholeSalePrice { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal NetPrice { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public decimal Quantity { get; set; }
        public double FractionAdjustment { get; set; }


    }
}
