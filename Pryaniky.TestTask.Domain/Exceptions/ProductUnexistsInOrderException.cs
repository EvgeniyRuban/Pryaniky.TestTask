using Pryaniky.TestTask.Domain.Entities;

namespace Pryaniky.TestTask.Domain.Exceptions
{
    public class ProductUnexistsInOrderException : Exception
    {
        public ProductUnexistsInOrderException() : base($"{nameof(Order)} has not contain {nameof(Product)} with selected 'id'.")
        { 
        }
    }
}
