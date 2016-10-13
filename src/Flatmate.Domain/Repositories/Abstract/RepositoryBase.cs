using Flatmate.Domain.Models.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flatmate.Domain.Repositories.Abstract
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : ModelBase
    {
        protected readonly IMongoCollection<T> _collection;

        public RepositoryBase(IMongoDatabase database)
        {
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> Find()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expresion)
        {
            return _collection.Find(expresion).ToList();
        }

        public T FindById(ModelId id)
        {
            return _collection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Insert(T item)
        {
            if (item.Id == ModelId.Empty)
                item.Id = ModelId.NewId();

            _collection.InsertOne(item);
        }

        public void Update(T item)
        {
            BsonDocument doc = item.ToBsonDocument();
            _collection.UpdateOne(x => x.Id == item.Id, doc);
        }

        public long UpdateWhere(Expression<Func<T, bool>> filter, Update<T> updateDefinition)
        {
            if (updateDefinition == null)
                throw new ArgumentNullException("updateDefinition");

            var firstDef = updateDefinition.FieldsWithValues.First();

            var mongoUpdDef = Builders<T>.Update.Set(firstDef.Key, firstDef.Value);    

            foreach (var item in updateDefinition.FieldsWithValues)
            {
                mongoUpdDef = mongoUpdDef.Set(item.Key, item.Value);
            }

            return _collection.UpdateMany(filter, mongoUpdDef).MatchedCount;
        }

        public long DeleteWhere(Expression<Func<T, bool>> filter)
        {
            return _collection.DeleteMany(filter).DeletedCount;
        }
    }
}
