using System;
using System.Threading;
using System.Threading.Tasks;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp
{
    public class StashRequest
    {
        private readonly Func<CancellationToken, Task> _workItem;

        public StashRequest(Func<CancellationToken, Task> workItem)
        {
            _workItem = workItem;
        }

        public async Task<StashResponse> ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _workItem(cancellationToken).ConfigureAwait(false);
            return new StashResponse();
        }
    }

    public class StashRequest<T>
    {
        private readonly Func<CancellationToken, Task<T>> _workItem;

        public StashRequest(Func<CancellationToken, Task<T>> workItem)
        {
            _workItem = workItem;
        }

        public async Task<StashResponse<T>> ExecuteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return new StashResponse<T>(await _workItem(cancellationToken).ConfigureAwait(false));
        }
    }

    public class StashPaginatedRequest<T>
    {
        private readonly Func<int, int, CancellationToken, Task<Pagination<T>>> _workItem;

        internal StashPaginatedRequest(Func<int, int, CancellationToken, Task<Pagination<T>>> workItem)
        {
            _workItem = workItem;
        }

        public async Task<StashResponse<Pagination<T>>> ExecuteAsync(int start = 0, int limit = 25, CancellationToken cancellationToken = default(CancellationToken))
        {
            return new StashResponse<Pagination<T>>(await _workItem(start, limit, cancellationToken).ConfigureAwait(false));
        }
    }
}
