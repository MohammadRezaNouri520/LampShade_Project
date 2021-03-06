using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public ProductPictureSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        [NeedsPermission(ShopPermissions.ListProductPictures)]
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Create", command);
        }

        [NeedsPermission(ShopPermissions.CreateProductPicture)]
        public IActionResult OnPostCreate(CreateProductPicture command)
        {
            if(ModelState.IsValid)
            {
                var result = _productPictureApplication.Create(command);
                return new JsonResult(result);
            }
            return new JsonResult(false);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _productPictureApplication.GetDetails(id);
            command.Products = _productApplication.GetProducts();
            return Partial("./Edit", command);
        }

        [NeedsPermission(ShopPermissions.EditProductPicture)]
        public IActionResult OnPostEdit(EditProductPicture command)
        {
            if(ModelState.IsValid)
            {
                var result = _productPictureApplication.Edit(command);
                return new JsonResult(result);
            }
            return new JsonResult(false);
        }

        [NeedsPermission(ShopPermissions.RemoveProductPicture)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        [NeedsPermission(ShopPermissions.RestoreProductPicture)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);

            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
