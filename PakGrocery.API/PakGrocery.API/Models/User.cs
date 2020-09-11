using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string ShippingAddress { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string PostalCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string BusinessRegNo { get; set; }
        public string StoreLogo { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
        public int UserTypeId { get; set; }

    }
}
