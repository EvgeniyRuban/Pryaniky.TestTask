using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class ProductUpdateRequest : IUpdateRequest<Product, Guid>
    {
        [Required]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public string? Summory { get; set; } 
    }
}
