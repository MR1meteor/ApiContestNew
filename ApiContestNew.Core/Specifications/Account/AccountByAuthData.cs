namespace ApiContestNew.Core.Specifications.Account
{
    public class AccountByAuthData : BaseSpecification<Models.Entities.Account>
    {
        public AccountByAuthData(string login, string password)
            : base(a => a.Email == login && a.Password == password)
        {

        }
    }
}
