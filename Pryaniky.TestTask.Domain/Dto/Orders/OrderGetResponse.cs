using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;
using Pryaniky.TestTask.Domain.Models;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class OrderGetResponse : IGetResponse<Order, Guid>
    {
        public Guid Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastUpdateAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? ComplitedAt { get; set; }
        public string? Summory { get; set; }
        public ICollection<ProductGetResponse> Products { get; set; }
    }
}
