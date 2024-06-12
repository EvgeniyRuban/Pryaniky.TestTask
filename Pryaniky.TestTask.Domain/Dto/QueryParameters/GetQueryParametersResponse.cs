namespace Pryaniky.TestTask.Domain.Dto
{
    public class GetQueryParametersResponse
    {
        public SortParametersResponse SortParameters { get; set; }
        public PaginationParametersResponse PaginationParameters { get; set; }
    }
}