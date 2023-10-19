
namespace ItemManagment.Helpers
{
    public class PagedResult<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        public PagedResult(T data, PaginationInfo paginationInfo)
        {
            this.PageNumber = paginationInfo.CurrentPage;
            this.PageSize = paginationInfo.PageSize;
            this.TotalRecords = paginationInfo.TotalItems;
            this.TotalPages = paginationInfo.TotalPages;

            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
        public PagedResult()
        {
            this.Message = string.Empty;
            this.Succeeded = false;
            this.Errors = null;
        }
    }
}
