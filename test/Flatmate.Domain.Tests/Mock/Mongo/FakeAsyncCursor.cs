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
        private readonly TProjection[] _items;

        private TProjection _current;

        private int _index = 0;

        public FakeAsyncCursor(IEnumerable<TProjection> items)
        {
            _items = items.ToArray();
        }

        public IEnumerable<TProjection> Current
        {
            get
            {
                return new TProjection[] { _current };
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_index < _items.Length)
            {
                _current = _items[_index];
                _index++;

                return true;
            }

            return false;
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
