using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class ProductGetResponse : IGetResponse<Product, Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Summory { get; set; }
    }
}
