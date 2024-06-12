using Pryaniky.TestTask.Domain.Dto.Base;
using Pryaniky.TestTask.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Pryaniky.TestTask.Domain.Dto
{
    public class ProductCreateResponse : ICreateResponse<Product, Guid>
    {
        [Required]
        public Guid Id { get; set; }
    }
}
