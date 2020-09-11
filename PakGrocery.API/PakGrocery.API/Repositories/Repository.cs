using MongoDB.Driver;
using PakGrocery.API.Entities;
using PakGrocery.API.Helpers;
using PakGrocery.API.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected Repository(string collectionName) : this("mongodb://localhost:27017", "PakGrocery", collectionName)
        {

        }
        protected Repository(string conString, string dbName, string collectionName)
        {
            this.Database = new MongoClient(conString).GetDatabase(dbName);
            this.CollectionName = collectionName;
            this.Collection = (MongoCollectionBase<TEntity>)this.Database.GetCollection<TEntity>(this.CollectionName);
        }

        public IMongoDatabase Database { get;  private set; }
        public MongoCollectionBase<TEntity> Collection { get; private set; }
        public string CollectionName { get; private set; }

        public long TotalRecords => this.Collection.CountDocuments(Entities => true);

        public bool Delete(TEntity entity)
        {
            var result = this.Collection.DeleteOne(ent => ent.Id == entity.Id);
            if (result.IsAcknowledged)
            {
                return (result.DeletedCount > 0);
            }
            return false;
        }

        public bool Delete(string id)
        {
            var result = this.Collection.DeleteOne(ent => ent.Id == id);
            if (result.IsAcknowledged)
            {
                return (result.DeletedCount > 0);
            }
            return false;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = this.Collection.Find<TEntity>(predicate).ToEnumerable();
            return entities;
        }

        public TEntity Find(string id)
        {
            var entity = this.Collection.Find(ent => ent.Id == id).FirstOrDefault();
            return entity;
        }

        public IEnumerable<TEntity> GetAll(string sort)
        {
            var items = this.Collection.AsQueryable().ApplySort(sort).AsEnumerable();

            return items;
        }

        public TEntity Insert(TEntity entity)
        {
            try
            {
                this.Collection.InsertOne(entity);
                var item = this.Collection.Find(ent => ent.Id == entity.Id).FirstOrDefault();

                return item;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(IEnumerable<TEntity> entities)
        {
            try
            {
                this.Collection.InsertMany(entities);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(string id, TEntity entity)
        {
            var item = this.Collection.ReplaceOne(ent => ent.Id == id, entity);

            if (item.IsAcknowledged)
                return true;
            else
                return false;

        }
    }
}
