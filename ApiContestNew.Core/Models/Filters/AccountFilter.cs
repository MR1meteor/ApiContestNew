namespace ApiContestNew.Core.Models.Filters
{
    public class AccountFilter
    {
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Email { get; set; } = null;
        public int From { get; set; } = 0;
        public int Size { get; set; } = 10;

        public AccountFilter()
        {

        }

        public AccountFilter(
            string? firstName, string? lastName,
            string? email, int from, int size)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            From = from;
            Size = size;
        }

        public bool IsValid()
        {
            if (From < 0 || Size <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
