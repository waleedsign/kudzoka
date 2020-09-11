using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Entities
{
    public class Store : EntityBase
    {
        public string StoreName { get; set; }
        public string StoreLogo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string PostalCode { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}
