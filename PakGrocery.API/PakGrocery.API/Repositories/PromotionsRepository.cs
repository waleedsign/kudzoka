using PakGrocery.API.Entities;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories
{
    public class PromotionsRepository : Repository<Promotion>,IPromotionsRepository
    {
        public PromotionsRepository() : base("Promotions")
        {

        }
    }
}
