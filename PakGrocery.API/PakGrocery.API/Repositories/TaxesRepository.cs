using PakGrocery.API.Entities;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories
{
    public class TaxesRepository : Repository<Tax>,ITaxesRepository
    {
        public TaxesRepository() : base("Taxes")
        {

        }
    }
}
