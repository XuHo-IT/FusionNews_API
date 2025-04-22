using System.Net;

namespace Application.Reponse
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool isSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }

        public int currentPage { get; set; }
        public int totalPages { get; set; }
        public int pageSize { get; set; } //items per page
        public int totalRecords { get; set; } // total items
    }
}
