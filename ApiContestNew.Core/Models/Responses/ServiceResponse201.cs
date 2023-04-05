using System.Net;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse201<T> : ServiceResponse<T>
    {
        public ServiceResponse201(
            HttpStatusCode statusCode = HttpStatusCode.Created,
            T? data = default,
            string message = "Successfully created") : base(statusCode, data, message)
        {

        }
    }
}
