namespace ApiContestNew.Core.Models.Entities
{
    public class Account : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public List<Animal> ChippedAnimals { get; set; } = new();
    }
}
