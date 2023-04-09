namespace ApiContestNew.Core.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<bool> Authenticate(string username, string password);
    }
}
