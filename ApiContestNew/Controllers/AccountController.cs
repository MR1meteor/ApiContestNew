using ApiContestNew.Core.Interfaces.Services;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Models.Filters;
using ApiContestNew.Dtos.Account;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiContestNew.Controllers
{
    [Route("accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        
        [HttpGet("{accountId}")]
        public async Task<ActionResult<GetAccountDto>> GetAccount(int accountId)
        {
            var response = await _accountService.GetAccountAsync(accountId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAccountDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.NotFound => NotFound(),
                _ => StatusCode(500),
            };
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<GetAccountDto>>> GetAccounts([FromQuery] AccountFilter filter)
        {
            var response = await _accountService.GetAccountsAsync(filter);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<List<GetAccountDto>>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                _ => StatusCode(500),
            };
        }

        [HttpPut("{accountId}")]
        public async Task<ActionResult<GetAccountDto>> UpdateAccount(int accountId, UpdateAccountDto dto)
        {
            if (string.IsNullOrWhiteSpace(Request.Headers.Authorization))
            {
                return Unauthorized();
            }

            var response = await _accountService.UpdateAccountAsync(accountId, _mapper.Map<Account>(dto));

            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(_mapper.Map<GetAccountDto>(response.Data)),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Forbidden => StatusCode(403),
                HttpStatusCode.Conflict => Conflict(),
                _ => StatusCode(500),
            };
        }

        [HttpDelete("{accountId}")]
        public async Task<ActionResult<GetAccountDto>> DeleteAccount(int accountId)
        {
            if (string.IsNullOrWhiteSpace(Request.Headers.Authorization))
            {
                return Unauthorized();
            }

            var response = await _accountService.DeleteAccountAsync(accountId);

            return response.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode(200),
                HttpStatusCode.BadRequest => BadRequest(),
                HttpStatusCode.Forbidden => StatusCode(403),
                _ => StatusCode(500),
            };
        }
    }
}
