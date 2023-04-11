namespace ApiContestNew.Core.Specifications.AnimalType
{
    public class TypeByIds : BaseSpecification<Models.Entities.AnimalType>
    {
        public TypeByIds(long[] ids)
            : base(t => ids.Contains(t.Id))
        {

        }
    }
}
