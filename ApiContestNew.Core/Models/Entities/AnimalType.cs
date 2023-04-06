namespace ApiContestNew.Core.Models.Entities
{
    public class AnimalType
    {
        public long Id { get; set; }
        public string Type { get; set; } = string.Empty;

        public ICollection<Animal> Animals { get; set; } = new List<Animal>();

        public AnimalType()
        {
            
        }

        public AnimalType(long id, string type)
        {
            Id = id;
            Type = type;
        }

        public bool IsValid()
        {
            if (Id <= 0 || !IsValidWithoutId())
            {
                return false;
            }

            return true;
        }

        public bool IsValidWithoutId()
        {
            if (String.IsNullOrWhiteSpace(Type))
            {
                return false;
            }

            return true;
        }
    }
}
