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
    }
}
