using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain
{
    public class ModelIdBsonSerializer : IBsonSerializer
    {
        public Type ValueType {get { return typeof(ModelId); } }

        public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            return new ModelId(context.Reader.ReadBytes());
        }

        public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
        {
            context.Writer.WriteBytes(((ModelId)value).ToByteArray());
        }
    }
}
