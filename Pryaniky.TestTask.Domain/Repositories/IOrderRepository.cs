using Pryaniky.TestTask.Domain.Dto;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Repositories
{
    public interface IOrderRepository : ICrudRepository<
        Order,
        Guid,
        OrderCreateRequest,
        OrderUpdateRequest,
        OrderDeleteRequest,
        OrderGetRequest,
        OrderCreateResponse,
        OrderGetResponse>
    { 
        Task<OrderGetQueryResponse> GetByQuery(OrderGetQueryRequest request, CancellationToken cancellationToken);
        Task AddProduct(Guid id, Guid productId, CancellationToken cancellationToken);
        Task DeleteProduct(Guid id, Guid productId, CancellationToken cancellationToken);
    }
}