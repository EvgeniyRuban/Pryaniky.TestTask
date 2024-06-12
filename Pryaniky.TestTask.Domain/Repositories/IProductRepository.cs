using Pryaniky.TestTask.Domain.Dto;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Repositories
{
    public interface IProductRepository : ICrudRepository<
        Product, 
        Guid, 
        ProductCreateRequest, 
        ProductUpdateRequest, 
        ProductDeleteRequest, 
        ProductGetRequest, 
        ProductCreateResponse,
        ProductGetResponse>
    {
        Task<ProductGetQueryResponse> GetByQuery(ProductGetQueryRequest request, CancellationToken cancellationToken);
    }
}