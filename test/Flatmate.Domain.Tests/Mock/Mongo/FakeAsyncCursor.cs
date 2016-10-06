using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace Flatmate.Domain.Tests.Mock.Mongo
{
    //TProjection
    public class FakeAsyncCursor<TProjection> : IAsyncCursor<TProjection>
    {
        public IEnumerable<TProjection> Current
        {
            get
            {
                return new List<TProjection>();
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext(CancellationToken cancellationToken = default(CancellationToken))
        {
            return false;
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
