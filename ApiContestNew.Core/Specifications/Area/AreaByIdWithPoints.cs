namespace ApiContestNew.Core.Specifications.Area
{
    public class AreaByIdWithPoints : BaseSpecification<Models.Entities.Area>
    {
        public AreaByIdWithPoints(long id)
            : base(a => a.Id == id)
        {
            AddInclude(a => a.AreaPoints);
        }
    }
}
