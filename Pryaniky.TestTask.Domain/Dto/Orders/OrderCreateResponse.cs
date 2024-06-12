using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class OrderCreateResponse : ICreateResponse<Order, Guid>
    {
        public Guid Id { get; set; }
    }
}
