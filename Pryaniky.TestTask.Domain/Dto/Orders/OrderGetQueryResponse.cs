namespace Pryaniky.TestTask.Domain.Dto
{
    public class OrderGetQueryResponse
    {
        public GetQueryParametersRequest RequestParameters { get; set; }
        public GetQueryParametersResponse ResponseParameters { get; set; }
        public ICollection<OrderGetResponse> Orders { get; set; }
    }
}
