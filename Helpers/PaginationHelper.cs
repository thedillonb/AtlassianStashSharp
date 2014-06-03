using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AtlassianStashSharp.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<List<TModel>> GetAll<TModel>(StashPaginatedRequest<TModel> request)
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
    }
}

