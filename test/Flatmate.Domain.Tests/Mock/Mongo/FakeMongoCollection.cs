using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Threading;

namespace Flatmate.Domain.Tests.Mock.Mongo
{
    public class FakeMongoCollection<T> : IMongoCollection<T>
        where T: class, new()
    {
        public CollectionNamespace CollectionNamespace
        {
            get
            {
                return new CollectionNamespace("test", typeof(T).Name);
            }
        }

        public IMongoDatabase Database
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IBsonSerializer<T> DocumentSerializer
        {
            get
            {
                return new FakeBsonSerializer<T>();
            }
        }

        public IMongoIndexManager<T> Indexes
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public MongoCollectionSettings Settings
        {
            get
            {
                return new MongoCollectionSettings();
            }
        }

        public IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<T, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public BulkWriteResult<T> BulkWrite(IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<BulkWriteResult<T>> BulkWriteAsync(IEnumerable<WriteModel<T>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public long Count(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<long> CountAsync(FilterDefinition<T> filter, CountOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteMany(FilterDefinition<T> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public DeleteResult DeleteOne(FilterDefinition<T> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TField> Distinct<TField>(FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<T, TField> field, FilterDefinition<T> filter, DistinctOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndDelete<TProjection>(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<T> filter, FindOneAndDeleteOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndReplace<TProjection>(FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<T> filter, T replacement, FindOneAndReplaceOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public TProjection FindOneAndUpdate<TProjection>(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<T> filter, FindOptions<T, TProjection> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new FakeAsyncCursor<TProjection>();
        }

        public void InsertMany(IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task InsertManyAsync(IEnumerable<T> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void InsertOne(T document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(T document, CancellationToken _cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(T document, InsertOneOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<T, TResult> options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : T
        {
            throw new NotImplementedException();
        }

        public ReplaceOneResult ReplaceOne(FilterDefinition<T> filter, T replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<T> filter, T replacement, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<T> WithReadConcern(ReadConcern readConcern)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<T> WithReadPreference(ReadPreference readPreference)
        {
            throw new NotImplementedException();
        }

        public IMongoCollection<T> WithWriteConcern(WriteConcern writeConcern)
        {
            throw new NotImplementedException();
        }
    }
}
