using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.ListInventory, "مشاهده لیست انبار"),
                        new PermissionDto(InventoryPermissions.SearchInventory, "جستجو در انبار"),
                        new PermissionDto(InventoryPermissions.CreateInventory, "ایجاد انبار جدید"),
                        new PermissionDto(InventoryPermissions.EditInventory, "ویرایش انبار"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory, "افزایش موجودی انبار"),
                        new PermissionDto(InventoryPermissions.ReduceInventory, "کاهش موجودی انبار"),
                        new PermissionDto(InventoryPermissions.InventoryOperationLog, "مشاهده گردش انبار")
                    }
                },
            };
        }
    }
}
