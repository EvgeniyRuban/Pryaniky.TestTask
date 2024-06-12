namespace Pryaniky.TestTask.Domain.Dto
{
    public class GetQueryParametersRequest
    {
        public SortParametersRequest SortParameters { get; set; } = new();
        public PaginationParametersRequest PaginationParameters { get; set; } = new();
    }
}