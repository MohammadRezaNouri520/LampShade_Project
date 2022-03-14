namespace AccountManagement.Domain.RoleAgg
{
    public class RolePermissions
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public int Code { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public RolePermissions(int code)
        {
            Code = code;
        }
    }
}
