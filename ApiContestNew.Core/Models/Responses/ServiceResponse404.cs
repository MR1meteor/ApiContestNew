using System.Net;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse404<T> : ServiceResponse<T>
    {
        public ServiceResponse404(
            HttpStatusCode statusCode = HttpStatusCode.NotFound,
            string message = "Object not found") : base(statusCode, message: message) 
        {
            
        }
    }
}
