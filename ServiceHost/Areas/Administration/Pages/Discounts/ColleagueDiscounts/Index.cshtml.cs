using System.Collections.Generic;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts
{
    public class IndexModel : PageModel
    {
        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        public SelectList Products { get; set; }
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetDefine()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("./Define", command);
        }

        public JsonResult OnPostDefine(DefineColleagueDiscount command)
        {
            var resutl = _colleagueDiscountApplication.Define(command);
            return new JsonResult(resutl);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _colleagueDiscountApplication.GetDetails(id);
            command.Products = _productApplication.GetProducts();
            return Partial("./Edit", command);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetActive(long id)
        {
            var result = _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetInActive(long id)
        {
            var result = _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }
    }
}
