using System.Net;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse400<T> : ServiceResponse<T>
    {
        public ServiceResponse400(
            HttpStatusCode statusCode = HttpStatusCode.BadRequest,
            string message = "Wrong data") : base(statusCode, message: message) 
        {
        
        }
    }
}
