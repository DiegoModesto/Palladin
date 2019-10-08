using System;
using System.Collections.Generic;
using System.Text;

namespace Palladin.Services.ApiContract.V1.Responses
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }

        public PagedResponse() { }
        public PagedResponse(IEnumerable<T> data)
        {
            this.Data = data;
        }
    }
}
