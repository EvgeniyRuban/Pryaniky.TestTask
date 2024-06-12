using Pryaniky.TestTask.Domain.Models;

namespace Pryaniky.TestTask.Domain.Entities
{
    public class Order : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastUpdateAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset? ComplitedAt { get; set; }
        public string? Summory { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
