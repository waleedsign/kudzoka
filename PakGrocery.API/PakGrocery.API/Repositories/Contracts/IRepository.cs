using PakGrocery.API.Entities;
using PakGrocery.API.Helpers;
using PakGrocery.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories.Contracts
{
    public interface IRepository<TEntity> where TEntity: EntityBase
    {
        bool Update(string id,TEntity entity);
        IEnumerable<TEntity> GetAll(string sort);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(string id);
        bool Delete(TEntity entity);
        bool Delete(string id);
        TEntity Insert(TEntity entity);
        bool Insert(IEnumerable<TEntity> entities);
        long TotalRecords { get; }
    }
}
