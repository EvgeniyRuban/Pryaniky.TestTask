using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class OrderUpdateRequest : IUpdateRequest<Order, Guid>
    {
        public Guid Id { get; set; }
        public string? Summory { get; set; }
    }
}
