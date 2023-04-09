using ApiContestNew.Core.Interfaces.Repositories;
using ApiContestNew.Core.Models.Entities;
using ApiContestNew.Core.Specifications.Account;
using ApiContestNew.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ApiContestNew.Infrastructure.Repositories
{
    public class AuthenticationRepository : BaseRepository<Account>, IAuthenticationRepository
    {
        public AuthenticationRepository(DataContext dataContext)
            : base(dataContext)
        {

        }

        async public Task<bool> Authenticate(string username, string password)
        {
            string encodedPassword = EncodePassword(password);

            var account = await ApplySpecification(new AccountByAuthData(username, encodedPassword))
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return false;
            }

            return true;
        }

        private string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var encodedPassword = Convert.ToBase64String(bytes);
            return encodedPassword;
        }
    }
}
