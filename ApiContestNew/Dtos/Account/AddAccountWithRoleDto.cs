namespace ApiContestNew.Dtos.Account
{
    public record AddAccountWithRoleDto : AddAccountDto
    {
        public string Role { get; set; } = string.Empty;
    }
}
