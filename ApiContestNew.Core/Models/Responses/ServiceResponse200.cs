using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApiContestNew.Core.Models.Responses
{
    public class ServiceResponse200<T> : ServiceResponse<T>
    {
        public ServiceResponse200(
            HttpStatusCode statusCode = HttpStatusCode.OK,
            T? data = default,
            string message = "Successfully") : base(statusCode, data, message)
        {

        }
    }
}
