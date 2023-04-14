namespace ApiContestNew.Core.Specifications.Area
{
    public class AreaByNameWithPoints : BaseSpecification<Models.Entities.Area>
    {
        public AreaByNameWithPoints(string name)
            : base(a => a.Name == name)
        {
            AddInclude(a => a.AreaPoints);
        }
    }
}
