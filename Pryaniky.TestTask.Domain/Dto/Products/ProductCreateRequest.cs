using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class ProductCreateRequest : ICreateRequest<Product, Guid>
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Summory { get; set; }
    }
}
