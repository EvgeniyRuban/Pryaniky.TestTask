namespace Pryaniky.TestTask.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType) : base($"{entityType.Name} is not found.")
        {
        }
    }
}
