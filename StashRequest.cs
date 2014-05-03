using System;
using System.Threading.Tasks;
using AtlassianStashSharp.Models;

namespace AtlassianStashSharp
{
    public class StashRequest
    {
        private readonly Func<Task> _workItem;

        public StashRequest(Func<Task> workItem)
        {
            _workItem = workItem;
        }

        public async Task<StashResponse> ExecuteAsync()
        {
            await _workItem();
            return new StashResponse();
        }
    }

    public class StashRequest<T>
    {
        private readonly Func<Task<T>> _workItem;

        public StashRequest(Func<Task<T>> workItem)
        {
            _workItem = workItem;
        }

        public async Task<StashResponse<T>> ExecuteAsync()
        {
            return new StashResponse<T>(await _workItem());
        }
    }

    public class StashPaginatedRequest<T>
    {
        private readonly Func<int, int, Task<Pagination<T>>> _workItem;

        internal StashPaginatedRequest(Func<int, int, Task<Pagination<T>>> workItem)
        {
            _workItem = workItem;
        }

        public async Task<StashResponse<Pagination<T>>> ExecuteAsync(int start = 0, int limit = 25)
        {
            return new StashResponse<Pagination<T>>(await _workItem(start, limit));
        }
    }
}
