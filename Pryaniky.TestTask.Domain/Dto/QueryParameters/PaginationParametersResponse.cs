namespace Pryaniky.TestTask.Domain.Dto
{
    public class PaginationParametersResponse
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }  
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
    }
}