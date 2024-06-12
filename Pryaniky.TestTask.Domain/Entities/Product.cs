namespace Pryaniky.TestTask.Domain.Entities
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string? Summory { get; set; }
        public Order? Order { get; set; }
    }
}
