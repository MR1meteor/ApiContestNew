using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Dtos.Account;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Route("")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost("registration")]
        public async Task<ActionResult<GetAccountDto>> Register(AddAccountDto dto)
        {
            if (!string.IsNullOrWhiteSpace(Request.Headers.Authorization))
            {
                return StatusCode(403);
            }

            var response = await _authenticationService.Register(_mapper.Map<Account>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.Created => StatusCode(201, _mapper.Map<GetAccountDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }
    }
}
