using System;

namespace Palladin.Services.ApiContract.V1.Request.Queries
{
    public class PaginationQuery
    {
        public Guid FilterId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationQuery()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
