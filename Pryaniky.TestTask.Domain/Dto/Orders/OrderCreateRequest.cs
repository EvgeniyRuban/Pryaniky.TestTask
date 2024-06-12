using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class OrderCreateRequest : ICreateRequest<Order, Guid>
    {
        public string? Summory { get; set; }
    }
}
