using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace DiscountManagement.Infrastructure.Configuration.Permissions
{
    public class DiscountPermissionsExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Colleague_Discounts", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListColleagueDiscounts,"لیست تخفیفات همکاران"),
                        new PermissionDto(DiscountPermissions.SearchColleagueDiscounts,"جستجو در تخفیفات همکاران"),
                        new PermissionDto(DiscountPermissions.DefineColleagueDiscount,"تعریف تخفیف همکاران"),
                        new PermissionDto(DiscountPermissions.EditColleagueDiscount,"ویرایش تخفیف همکاران"),
                        new PermissionDto(DiscountPermissions.RemoveColleagueDiscount,"حذف تخفیف همکاران"),
                        new PermissionDto(DiscountPermissions.RestoreColleagueDiscount,"بازیابی تخفیف همکاران"),
                    }
                },
                {
                    "Customer_Discounts", new List<PermissionDto>
                    {
                        new PermissionDto(DiscountPermissions.ListCustomerDiscounts, "لیست تخفیفات کاربران"),
                        new PermissionDto(DiscountPermissions.SearchCustomerDiscounts, "جستجو در تخفیفات کاربران"),
                        new PermissionDto(DiscountPermissions.DefineCustomerDiscounts, "تعریف تخفیف کاربران"),
                        new PermissionDto(DiscountPermissions.EditCustomerDiscounts, "ویرایش تخفیف کاربران"),
                        new PermissionDto(DiscountPermissions.ActiveCustomerDiscounts, "فعالسازی تخفیف کاربران"),
                        new PermissionDto(DiscountPermissions.InactiveCustomerDiscounts, "غیرفعالسازی تخفیف کاربران"),
                    }
                }
            };
        }
    }
}
