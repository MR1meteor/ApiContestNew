namespace ApiContestNew.Core.Models.Entities
{
    public class AnimalType
    {
        public long Id { get; set; }
        public string Type { get; set; } = string.Empty;

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
