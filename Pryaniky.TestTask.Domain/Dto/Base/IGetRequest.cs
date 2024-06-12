using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Dto.Base
{
    public interface IGetRequest<TEntity, TId> : IEntityDto<TEntity, TId>
        where TEntity : IEntity<TId>
    {
    }
}
