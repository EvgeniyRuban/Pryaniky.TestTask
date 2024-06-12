using Microsoft.EntityFrameworkCore;
using Pryaniky.TestTask.DAL.Extentions;
using Pryaniky.TestTask.Domain.Dto;
using Pryaniky.TestTask.Domain.Entities;
using Pryaniky.TestTask.Domain.Exceptions;
using Pryaniky.TestTask.Domain.Models;
using Pryaniky.TestTask.Domain.Repositories;

namespace Pryaniky.TestTask.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderCreateResponse> Add(OrderCreateRequest request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Summory = request.Summory,
                CreatedAt = DateTimeOffset.UtcNow,
                Status = OrderStatus.InWork
            };

            var newEntity = await _context.AddAsync(order, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new OrderCreateResponse
            {
                Id = newEntity.Entity.Id,
            };

            return response;
        }
        public async Task Delete(OrderDeleteRequest request, CancellationToken cancellationToken)
        {
            var entityToDelete = await _context.Orders.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entityToDelete is null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            var dateTimeNow = DateTimeOffset.UtcNow;
            entityToDelete.LastUpdateAt = dateTimeNow;
            entityToDelete.DeletedAt = dateTimeNow;
            entityToDelete.Status = request.IsSuccess ? OrderStatus.Complited : OrderStatus.Cancelled;
            entityToDelete.Products = null!;

            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<OrderGetResponse> GetById(OrderGetRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders.Where(e => e.Id == request.Id)
                .Include(e => e.Products)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            var response = new OrderGetResponse
            {
                Id = entity.Id,
                OrderStatus = entity.Status,
                CreatedAt = entity.CreatedAt,
                LastUpdateAt = entity.LastUpdateAt,
                DeletedAt = entity.DeletedAt,
                ComplitedAt = entity.ComplitedAt,
                Summory = entity.Summory,
            };

            response.Products = new List<ProductGetResponse>(entity.Products.Count);

            foreach (var product in entity.Products)
            {
                response.Products.Add(new ProductGetResponse
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Summory = product.Summory,
                });
            }

            return response;
        }
        public async Task Update(OrderUpdateRequest request, CancellationToken cancellationToken)
        {
            var entity = await _context.Orders.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (entity is null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            entity.LastUpdateAt = DateTimeOffset.UtcNow;
            entity.Summory = request.Summory ?? entity.Summory;

            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<OrderGetQueryResponse> GetByQuery(OrderGetQueryRequest request, CancellationToken cancellationToken)
        {
            var pagedQuery = _context.Orders
                .Paging<Order, Guid>(
                pageNumber: request.Query.PaginationParameters.PageNumber,
                pageSize: request.Query.PaginationParameters.PageSize)
                .Include(o => o.Products);

            var orderedQuery = request.Query.SortParameters.IsDescending
                ? pagedQuery.OrderByDescending(p => p.Id)
                : pagedQuery.OrderBy(p => p.Id);

            var orderList = await orderedQuery.ToListAsync(cancellationToken);
            var orderTotal = _context.Orders.Count();
            var pagesTotal = orderTotal / request.Query.PaginationParameters.PageSize;

            if (orderTotal % request.Query.PaginationParameters.PageSize > 0)
            {
                pagesTotal++;
            }

            var response = new OrderGetQueryResponse
            {
                RequestParameters = request.Query,
                ResponseParameters = new GetQueryParametersResponse
                {
                    PaginationParameters = new PaginationParametersResponse
                    {
                        PageSize = orderList.Count,
                        PageNumber = request.Query.PaginationParameters.PageNumber,
                        TotalPages = pagesTotal,
                        HasNext = pagesTotal > request.Query.PaginationParameters.PageNumber
                    },
                    SortParameters = new SortParametersResponse
                    {
                        IsDescending = request.Query.SortParameters.IsDescending
                    }
                },
                Orders = new List<OrderGetResponse>(orderList.Count)
            };

            for (int i = 0; i < orderList.Count; i++)
            {
                response.Orders.Add(new OrderGetResponse
                {
                    Id = orderList[i].Id,
                    Summory = orderList[i].Summory,
                    CreatedAt = orderList[i].CreatedAt,
                    LastUpdateAt = orderList[i].LastUpdateAt,
                    ComplitedAt = orderList[i].ComplitedAt,
                    DeletedAt = orderList[i].DeletedAt,
                    OrderStatus = orderList[i].Status,
                    Products = new List<ProductGetResponse>(orderList[i].Products.Count)
                });

                foreach (var product in orderList[i].Products)
                {
                    response.Orders.Last().Products.Add(new ProductGetResponse
                    {
                        Id = product.Id,
                        Title = product.Title,
                        Price = product.Price,
                        Summory = product.Summory,
                    });
                }
            }

            return response;
        }
        public async Task AddProduct(Guid id, Guid productId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            var productContainsInOrder = order.Products.Any(p => p.Id == id);

            if (productContainsInOrder)
            {
                throw new ProductContainsInOrderException();
            }

            var product = await _context.Products
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            if (product == null)
            {
                throw new EntityNotFoundException(typeof(Product));
            }
            else if (product.Order != null)
            {
                throw new ProductInUseException();
            }

            order.Products.Add(product);
            order.LastUpdateAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteProduct(Guid id, Guid productId, CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            var productContainsInOrder = order.Products.Any(p => p.Id == productId);

            if (!productContainsInOrder)
            {
                throw new ProductUnexistsInOrderException();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);

            order.LastUpdateAt = DateTimeOffset.UtcNow;
            order.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}