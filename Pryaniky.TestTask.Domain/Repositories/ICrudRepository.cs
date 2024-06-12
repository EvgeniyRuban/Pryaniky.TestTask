using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Repositories
{
    public interface ICrudRepository<TEntity, TId, TCreateRequest, TUpdateRequest, TDeleteRequest, TGetRequest, TCreateResponse, TGetResponse>
        where TEntity : IEntity<TId>
        where TCreateRequest : ICreateRequest<TEntity, TId>
        where TUpdateRequest : IUpdateRequest<TEntity, TId>
        where TDeleteRequest : IDeleteRequest<TEntity, TId>
        where TGetRequest : IGetRequest<TEntity, TId>
        where TCreateResponse : ICreateResponse<TEntity, TId>
        where TGetResponse : IGetResponse<TEntity, TId>
    {
        Task<TCreateResponse> Add(TCreateRequest request, CancellationToken cancellationToken);
        Task<TGetResponse> GetById(TGetRequest request, CancellationToken cancellationToken);
        Task Update(TUpdateRequest request, CancellationToken cancellationToken);
        Task Delete(TDeleteRequest request, CancellationToken cancellationToken);
    }
}