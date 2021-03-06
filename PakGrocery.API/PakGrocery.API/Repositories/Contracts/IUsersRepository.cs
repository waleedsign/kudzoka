﻿using PakGrocery.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories.Contracts
{
    public interface IUsersRepository : IRepository<User>
    {
        bool ActivateRider(string id);
    }
}
