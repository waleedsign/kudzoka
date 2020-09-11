using PakGrocery.API.Entities;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories
{
    public class ProductsRepository: Repository<Product>, IProductsRepository
    {
        public ProductsRepository(): base("Products")
        {

        }
    }
}
