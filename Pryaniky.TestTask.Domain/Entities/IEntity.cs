namespace Pryaniky.TestTask.Domain.Entities
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}
