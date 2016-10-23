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
        T FindById(ModelId id);
        long Count(Expression<Func<T, bool>> expresion);
        long Count();
        void Insert(T item);
        void Update(T item);
        long UpdateWhere(Expression<Func<T, bool>> filter, Update<T> updateDefinition);
        long DeleteWhere(Expression<Func<T, bool>> filter);
    }
}
