using Microsoft.EntityFrameworkCore;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.DAL.Extentions
{
    public static class DbSetExtentions
    {
        public static IQueryable<TEntity> Paging<TEntity, TId>(this DbSet<TEntity> dbSet, int pageNumber = 1, int pageSize = 10)
            where TEntity : class, IEntity<TId>
        {
            var itemsToSkip = pageNumber < 2 ? 0 : (pageNumber - 1) * pageSize;
            return dbSet.Skip(itemsToSkip)
                        .Take(pageSize);
        }
    }
}
