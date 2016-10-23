using Flatmate.Domain.Models;
using Flatmate.Domain.Repositories.Abstract;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain.Repositories
{
    public class FlatRepository: RepositoryBase<Flat>, IFlatRepository
    {
        public FlatRepository(IMongoDatabase database)
            :base(database)
        {
        }
    }
}
