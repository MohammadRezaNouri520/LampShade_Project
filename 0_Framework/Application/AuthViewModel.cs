using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public List<int> Permissions { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public bool RememberMe { get; set; }

        public AuthViewModel()
        {

        }
        public AuthViewModel(long id, string fullName, string userName,List<int> permissions, long roleId, string role, bool rememberMe = false)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Permissions = permissions;
            RoleId = roleId;
            Role = role;
            RememberMe = rememberMe;
        }
    }
}
