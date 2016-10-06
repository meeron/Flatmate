using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain.Models.Abstract
{
    public abstract class ModelBase
    {
        [BsonId]
        public string Id { get; set; }
    }
}
