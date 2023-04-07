namespace ApiContestNew.Core.Specifications.Account
{
    public class AccountByIdWithAnimals : BaseSpecification<Models.Entities.Account>
    {
        public AccountByIdWithAnimals(int id)
            : base(a => a.Id == id)
        {
            AddInclude(a => a.ChippedAnimals);
        }
    }
}
