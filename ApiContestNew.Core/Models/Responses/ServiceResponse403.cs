using System.Net;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse403<T> : ServiceResponse<T>
    {
        public ServiceResponse403(
            HttpStatusCode statusCode = HttpStatusCode.Forbidden,
            T? data = default,
            string message = "Wrong request") : base(statusCode, data, message)
        {
        
        }
    }
}
