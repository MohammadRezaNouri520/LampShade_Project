namespace AccountManagement.Infrastructure.Configuration.Permissions
{
    public static class AccountPermissions
    {
        // Account
        public const int ListAccounts = 70;
        public const int SearchAccounts = 71;
        public const int CreateAccount = 72;
        public const int EditAccount = 73;
        public const int RemoveAccount = 74;
        public const int RestoreAccount = 75;
        public const int ChangePassword = 76;

        // Role
        public const int ListRoles = 80;
        public const int CreateRole = 81;
        public const int EditRole = 82;
    }
}