using MongoDB.Driver;
using PakGrocery.API.Entities;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories
{
    public class UsersRepository : Repository<User>,IUsersRepository
    {
        public UsersRepository() : base("Users")
        {

        }

        public bool ActivateRider(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(ent => ent.Id, id);
            UpdateDefinition<User> update = Builders<User>.Update.Set(ent => ent.IsActive, true);

            var user = this.Collection.FindOneAndUpdate(filter, update);

            return user != null;
        }
    }
}
