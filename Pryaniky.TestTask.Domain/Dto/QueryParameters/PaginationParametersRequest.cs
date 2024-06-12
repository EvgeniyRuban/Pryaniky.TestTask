namespace Pryaniky.TestTask.Domain.Dto
{
    public class PaginationParametersRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}