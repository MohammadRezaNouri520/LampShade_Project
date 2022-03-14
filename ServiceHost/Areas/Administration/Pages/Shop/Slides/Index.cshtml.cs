using System.Collections.Generic;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public List<SlideViewModel> Slides { get; set; }

        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        [NeedsPermission(ShopPermissions.ListSlides)]
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Slides = _slideApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlide();
            return Partial("./Create", command);
        }

        [NeedsPermission(ShopPermissions.CreateSlide)]
        public IActionResult OnPostCreate(CreateSlide command)
        {
            if(ModelState.IsValid)
            {
                var result = _slideApplication.Create(command);
                return new JsonResult(result);
            }
            return new JsonResult(false);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _slideApplication.GetDetails(id);
            return Partial("./Edit", command);
        }

        [NeedsPermission(ShopPermissions.EditSlide)]
        public IActionResult OnPostEdit(EditSlide command)
        {
            if(ModelState.IsValid)
            {
                var result = _slideApplication.Edit(command);
                return new JsonResult(result);
            }
            return new JsonResult(false);
        }

        [NeedsPermission(ShopPermissions.RemoveSlide)]
        public IActionResult OnGetRemove(long id)
        {
            var result = _slideApplication.Remove(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

        [NeedsPermission(ShopPermissions.RestoreSlide)]
        public IActionResult OnGetRestore(long id)
        {
            var result = _slideApplication.Restore(id);
            if (result.IsSucceeded)
                return RedirectToPage("./Index");

            Message = result.Message;
            return RedirectToPage("./Index");
        }

       
    }
}
