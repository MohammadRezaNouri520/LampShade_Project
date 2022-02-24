namespace AccountManagement.Application.Contracts.Account
{
    public class EditAccount : RegisterAccount
    {
        public long Id { get; set; }

        public new string Password { get; set; }
        public new string RePassword { get; set; }
    }
}
