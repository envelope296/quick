using Microsoft.EntityFrameworkCore;
using Quick.BusinessLogic.Contracts.Requests.Base;
using Quick.BusinessLogic.Contracts.Responses.Common;

namespace Quick.DataAccess.Context.Extensions
{
    public static class EntityFrameworkQueryableExtensions
    {

        public static async Task<PageResponse<TSource>> ToPageResponseAsync<TSource>(
            this IQueryable<TSource> source,
            int page,
            int size,
            CancellationToken cancellationToken)
        {
            var skip = (page - 1) * size;
            var take = size;

            var totalCount = await source.CountAsync(cancellationToken);
            if (totalCount == 0)
            {
                return PageResponse<TSource>.Empty;
            }

            var items = await source.Skip(skip).Take(take).ToListAsync(cancellationToken);
            return new PageResponse<TSource>
            {
                Items = items,
                TotalCount = totalCount,
            };
        }

        public static Task<PageResponse<TSource>> ToPageResponseAsync<TSource>(
            this IQueryable<TSource> source, 
            BasePageRequest pageRequest,
            CancellationToken cancellationToken)
        {
            return source.ToPageResponseAsync(pageRequest.Page, pageRequest.Size, cancellationToken);
        }
    }
}
