using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Exceptions
{
    public class ProductContainsInOrderException : Exception
    {
        public ProductContainsInOrderException() : base($"{nameof(Product)} already contains in order.")
        {
        }
    }
}
