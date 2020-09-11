using PakGrocery.API.Entities;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories
{
    public class CategoriesRepository : Repository<Category> , ICategoriesRepository
    {
        public CategoriesRepository() : base("Categories")
        {

        }
    }
}
