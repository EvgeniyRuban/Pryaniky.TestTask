namespace Pryaniky.TestTask.Domain.Dto
{
    public class ProductGetQueryResponse
    {
        public GetQueryParametersRequest RequestParameters { get; set; }
        public GetQueryParametersResponse ResponseParameters { get; set; }
        public ICollection<ProductGetResponse> Products { get; set; }
    }
}