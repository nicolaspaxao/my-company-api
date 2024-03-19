namespace CompanyAPI.Models {
    public class HttpError {
        public int statusCode { get; set; }
        public string? message { get; set; }

        public HttpError( int statusCode, string? message )
        {
            this.statusCode = statusCode;
            this.message = message;
        }
    }
}
