namespace ApiContestNew.Core.Specifications.Account
{
    public class AccountByEmailWithAnimals : BaseSpecification<Models.Entities.Account>
    {
        public AccountByEmailWithAnimals(string email)
            : base(a => a.Email == email)
        {
            AddInclude(a => a.ChippedAnimals);
        }
    }
}
