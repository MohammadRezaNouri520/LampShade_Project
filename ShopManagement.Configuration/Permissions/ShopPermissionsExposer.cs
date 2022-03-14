using _0_Framework.Infrastructure;
using System.Collections.Generic;

namespace ShopManagement.Infrastructure.Configuration.Permissions
{
    public class ShopPermissionsExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>> 
            {
                {
                    "Products", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts, "لیست محصولات"),
                        new PermissionDto(ShopPermissions.SearchProducts, "جستجو در محصولات"),
                        new PermissionDto(ShopPermissions.CreateProduct, "افزودن محصول"),
                        new PermissionDto(ShopPermissions.EditProduct, "ویرایش محصول")
                    }
                },
                {
                    "Product_Categories", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductCategories, "لیست گروه محصولات"),
                        new PermissionDto(ShopPermissions.SearchProductCategories, "جستجو در گروه محصولات"),
                        new PermissionDto(ShopPermissions.CreateProductCategory, "افزودن گروه محصول"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "ویرایش گروه محصول")
                    }
                },
                {
                    "Product_Pictures", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProductPictures, "لیست تصاویر محصول"),
                        new PermissionDto(ShopPermissions.SearchProductPictures, "جستجو در تصاویر محصول"),
                        new PermissionDto(ShopPermissions.CreateProductPicture, "افزودن تصویر محصول"),
                        new PermissionDto(ShopPermissions.EditProductPicture, "ویرایش تصویر محصول"),
                        new PermissionDto(ShopPermissions.RemoveProductPicture, "حذف تصویر محصول"),
                        new PermissionDto(ShopPermissions.RestoreProductPicture, "بازیابی تصویر محصول")
                    }
                },
                {
                    "Slides", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListSlides, "لیست اسلایدها"),
                        new PermissionDto(ShopPermissions.CreateSlide, "افزودن اسلاید"),
                        new PermissionDto(ShopPermissions.EditSlide, "ویرایش اسلاید"),
                        new PermissionDto(ShopPermissions.RemoveSlide, "حذف اسلاید"),
                        new PermissionDto(ShopPermissions.RestoreSlide, "بازیابی اسلاید"),
                    }
                }
            };
        }
    }
}
