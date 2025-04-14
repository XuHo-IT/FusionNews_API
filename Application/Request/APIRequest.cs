namespace Application.Request
{
    namespace Application.Request
    {
        public class APIRequest
        {
            public APIRequest()
            {
                Headers = new Dictionary<string, string>();
            }

            public string Url { get; set; }
            public string Method { get; set; } // GET, POST, PUT, DELETE
            public object Data { get; set; } // For body payload 
            public Dictionary<string, string> Headers { get; set; }
        }
    }

}
