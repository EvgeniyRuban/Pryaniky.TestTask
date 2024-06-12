using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Dto.Base
{
    public interface ICreateRequest<TEntity, TId> : IEntityDto<TEntity, TId>
        where TEntity : IEntity<TId>
    {
    }
}
