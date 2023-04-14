namespace ApiContestNew.Core.Models.Entities
{
    public class Account : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public List<Animal> ChippedAnimals { get; set; } = new();
    
        public Account()
        {

        }

        public Account(
            int id, string firstName,
            string lastName, string email,
            string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
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
            if(string.IsNullOrWhiteSpace(FirstName) ||
               string.IsNullOrWhiteSpace(LastName) ||
               string.IsNullOrWhiteSpace(Password) ||
               !EmailIsValid(Email))
            {
                return false;
            }

            return true;
        }

        private bool EmailIsValid(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                if (mail.Address == email)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
