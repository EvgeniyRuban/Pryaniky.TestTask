using Microsoft.EntityFrameworkCore;
using Pryaniky.TestTask.DAL.Extentions;
using Pryaniky.TestTask.Domain.Dto;
using Pryaniky.TestTask.Domain.Entities;
using Pryaniky.TestTask.Domain.Repositories;

namespace Pryaniky.TestTask.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ProductCreateResponse> Add(ProductCreateRequest request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Title = request.Title,
                Price = request.Price,
                Summory = request.Summory,
            };

            var newEntity = await _context.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new ProductCreateResponse
            {
                Id = newEntity.Entity.Id,
            };

            return response;
        }
        public async Task Delete(ProductDeleteRequest request, CancellationToken cancellationToken)
        {
            var entityToDelete = await _context.Products.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entityToDelete is null)
            {
                throw new Exception("Entity is not found.");
            }

            _context.Products.Remove(entityToDelete);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ProductGetResponse> GetById(ProductGetRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                throw new Exception("Entity is not found.");
            }

            var response = new ProductGetResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Price = entity.Price,
                Summory = entity.Summory,
            };

            return response;
        }
        public async Task Update(ProductUpdateRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                throw new Exception("Entity is not found.");
            }

            entity.Title = request.Title ?? entity.Title;
            entity.Summory = request.Summory ?? entity.Summory;
            entity.Price = request.Price ?? entity.Price;

            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ProductGetQueryResponse> GetByQuery(ProductGetQueryRequest request, CancellationToken cancellationToken)
        {
            var pagedQuery = _context.Products.Paging<Product, Guid>(
                pageNumber: request.Query.PaginationParameters.PageNumber,
                pageSize: request.Query.PaginationParameters.PageSize);

            var orderedQuery = request.Query.SortParameters.IsDescending
                ? pagedQuery.OrderByDescending(p => p.Id)
                : pagedQuery.OrderBy(p => p.Id);

            var productList = await orderedQuery.ToListAsync(cancellationToken);
            var productTotal = _context.Products.Count();
            var pagesTotal = productTotal / request.Query.PaginationParameters.PageSize;

            if (productTotal % request.Query.PaginationParameters.PageSize > 0)
            {
                pagesTotal++;
            }

            var response = new ProductGetQueryResponse
            {
                RequestParameters = request.Query,
                ResponseParameters = new GetQueryParametersResponse
                {
                    PaginationParameters = new PaginationParametersResponse
                    {
                        PageSize = productList.Count,
                        PageNumber = request.Query.PaginationParameters.PageNumber,
                        TotalPages = pagesTotal,
                        HasNext = pagesTotal > request.Query.PaginationParameters.PageNumber
                    },
                    SortParameters = new SortParametersResponse
                    {
                        IsDescending = request.Query.SortParameters.IsDescending
                    }
                },
                Products = new List<ProductGetResponse>(productList.Count)
            };

            foreach (var product in productList)
            {
                response.Products.Add(new ProductGetResponse
                {
                    Id = product.Id,
                    Price = product.Price,
                    Summory = product.Summory,
                    Title = product.Title,
                });
            }

            return response;
        }
    }
}