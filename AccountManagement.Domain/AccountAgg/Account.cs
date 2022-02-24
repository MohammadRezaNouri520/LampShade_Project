using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string ProfilePhoto { get; private set; }
        public bool IsRemoved { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

        public Account(string fullName, string userName, string mobile, string password, long roleId,
            string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Mobile = mobile;
            Password = password;
            RoleId = roleId;

            if (roleId == 0)
                RoleId = 1; //Sets the Account Role to "System User".
            
            ProfilePhoto = profilePhoto;
            IsRemoved = false;
        }

        public void Edit(string fullName, string userName, string mobile, long roleId,
            string profilePhoto)
        {
            FullName = fullName;
            UserName = userName;
            Mobile = mobile;
            RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
