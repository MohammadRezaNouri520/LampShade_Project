using System.Collections.Generic;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts
{
    public class IndexModel : PageModel
    {
        public CustomerDiscountSearchModel SearchModel { get; set; }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        public SelectList Products { get; set; }
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        [NeedsPermission(DiscountPermissions.ListCustomerDiscounts)]
        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetDefine()
        {
            var command = new DefineCustomerDiscount
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Define", command);
        }

        [NeedsPermission(DiscountPermissions.DefineCustomerDiscounts)]
        public JsonResult OnPostDefine(DefineCustomerDiscount command)
        {
            var resutl = _customerDiscountApplication.Define(command);
            return new JsonResult(resutl);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _customerDiscountApplication.GetDetails(id);
            command.Products = _productApplication.GetProducts();
            return Partial("./Edit", command);
        }

        [NeedsPermission(DiscountPermissions.EditCustomerDiscounts)]
        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        [NeedsPermission(DiscountPermissions.ActiveCustomerDiscounts)]
        public IActionResult OnGetActive(long id)
        {
            var result = _customerDiscountApplication.Active(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(DiscountPermissions.InactiveCustomerDiscounts)]
        public IActionResult OnGetInActive(long id)
        {
            var result = _customerDiscountApplication.Inactive(id);
            return RedirectToPage("./Index");
        }
    }
}
