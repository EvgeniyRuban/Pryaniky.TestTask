using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Exceptions
{
    public class ProductInUseException : Exception
    {
        public ProductInUseException() : base($"{nameof(Product)} in use.")
        {
        }
    }
}
