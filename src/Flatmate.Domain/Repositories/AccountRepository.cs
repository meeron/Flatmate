using Flatmate.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flatmate.Domain.Models;
using MongoDB.Driver;

namespace Flatmate.Domain.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IMongoDatabase database)
            :base(database)
        {
        } 
    }
}
