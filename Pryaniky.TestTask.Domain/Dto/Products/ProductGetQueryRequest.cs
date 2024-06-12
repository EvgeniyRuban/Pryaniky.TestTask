namespace Pryaniky.TestTask.Domain.Dto
{
    public class ProductGetQueryRequest
    {
        public GetQueryParametersRequest? Query { get; set; } = new();
    }
}