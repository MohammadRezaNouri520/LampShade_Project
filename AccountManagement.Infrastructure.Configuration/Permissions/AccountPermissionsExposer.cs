using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace AccountManagement.Infrastructure.Configuration.Permissions
{
    public class AccountPermissionsExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Users", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ListAccounts, "لیست کاربران"),
                        new PermissionDto(AccountPermissions.SearchAccounts, "جستجوی کاربران"),
                        new PermissionDto(AccountPermissions.CreateAccount, "افزودن کاربر"),
                        new PermissionDto(AccountPermissions.EditAccount, "ویرایش کاربر"),
                        new PermissionDto(AccountPermissions.RemoveAccount,"حذف کاربر"),
                        new PermissionDto(AccountPermissions.RestoreAccount,"بازیابی کاربر"),
                        new PermissionDto(AccountPermissions.ChangePassword,"تغییر رمز کاربر")
                    }
                },
                {
                    "Roles", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ListRoles,"لیست نقش ها"),
                        new PermissionDto(AccountPermissions.CreateRole,"ایجاد نقش جدید"),
                        new PermissionDto(AccountPermissions.EditRole,"ویرایش نقش کاربر")
                    }
                }
            };
        }
    }
}
