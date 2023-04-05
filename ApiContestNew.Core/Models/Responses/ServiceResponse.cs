using System.Net;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse<T>
    {
        public T? Data { get; }
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }

        public ServiceResponse(HttpStatusCode statusCode, T? data = default, string message = "")
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
        }
    }
}
