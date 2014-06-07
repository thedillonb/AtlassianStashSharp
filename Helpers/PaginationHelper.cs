using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AtlassianStashSharp.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<List<TModel>> ExecuteAsyncAll<TModel>(this StashPaginatedRequest<TModel> request)
        {
            var list = new List<TModel>();
            var start = 0;

            while (true)
            {
                var objects = await request.ExecuteAsync(start);
                if (objects.Data.Values.Count == 0)
                    break;

                list.AddRange(objects.Data.Values);
                if (objects.Data.IsLastPage || !objects.Data.NextPageStart.HasValue)
                    break;

                start = objects.Data.NextPageStart.Value;
            }

            return list;
        }

        public static async Task ExecuteAsyncAllBatched<T>(this StashPaginatedRequest<T> paginatedRequest, Action<List<T>> callback)
        {
            var start = 0;

            while (true)
            {
                var result = await paginatedRequest.ExecuteAsync(start);
                if (result.Data.Values.Count == 0)
                    return;

                callback(result.Data.Values);
                if (result.Data.IsLastPage || !result.Data.NextPageStart.HasValue)
                    return;

                start = result.Data.NextPageStart.Value;
            }
        }
    }
}

