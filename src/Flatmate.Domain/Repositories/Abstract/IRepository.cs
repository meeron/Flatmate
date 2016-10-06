using Flatmate.Domain.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Flatmate.Domain.Repositories.Abstract
{
    public interface IRepository<T>
        where T: ModelBase
    {
        IEnumerable<T> Find();
        IEnumerable<T> Find(Expression<Func<T, bool>> expresion);
    }
}
