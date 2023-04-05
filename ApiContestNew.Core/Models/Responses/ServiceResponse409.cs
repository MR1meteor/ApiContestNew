using System.Net;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse409<T> : ServiceResponse<T>
    {
        public ServiceResponse409(
            HttpStatusCode statusCode = HttpStatusCode.Conflict,
            string message = "There are conflicts") : base(statusCode, message: message)
        {

        }
    }
}
