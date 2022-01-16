using _01_LampshadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        [TempData]
        public string ResultMessage { get; set; }

        public ProductQueryModel Product { get; set; }
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Product = _productQuery.GetDetails(id);
        }

        public IActionResult OnPost(AddComment command, string productSlug)
        {
            if (ModelState.IsValid)
            {
                var result = _commentApplication.Add(command);
                //ResultMessage = result.Message;
                return RedirectToPage("/Product", new { id = productSlug });
            }
            return RedirectToPage("/Product", new { id = productSlug });
        }
    }
}
