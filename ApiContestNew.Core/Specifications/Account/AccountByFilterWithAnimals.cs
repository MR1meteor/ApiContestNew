using ApiContestNew.Core.Models.Filters;

namespace ApiContestNew.Core.Specifications.Account
{
    public class AccountByFilterWithAnimals : BaseSpecification<Models.Entities.Account>
    {
        public AccountByFilterWithAnimals(AccountFilter filter)
            : base(a => 
            (string.IsNullOrEmpty(filter.FirstName) || a.FirstName.ToLower().Contains(filter.FirstName.ToLower())) &&
            (string.IsNullOrEmpty(filter.LastName) || a.LastName.ToLower().Contains(filter.LastName.ToLower())) &&
            (string.IsNullOrEmpty(filter.Email) || a.Email.ToLower().Contains(filter.Email.ToLower())))
        {
            AddInclude(a => a.ChippedAnimals);
            AddOrderBy(a => a.Id);
            AddSkip(filter.From);
            AddTake(filter.Size);
        }
    }
}
