using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain.Tests.Mock.Mongo
{
    public class FakeBsonSerializer<T> : IBsonSerializer<T>
        where T: class, new()
    {
        public Type ValueType
        {
            get
            {
                return typeof(T);
            }
        }

        public T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new T();
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
        }

        object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new T();
        }
    }
}
