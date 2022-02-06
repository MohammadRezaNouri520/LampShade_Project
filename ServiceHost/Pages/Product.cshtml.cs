using _01_LampshadeQuery;
using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        [TempData]
        public string ResultMessage { get; set; }

        [BindProperty]
        public AddComment Comment { get; set; }

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
            Product = _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(AddComment comment, string productSlug)
        {
            if (ModelState.IsValid)
            {
                comment.Type = CommentType.Product;
                var result = _commentApplication.Add(comment);
                //ResultMessage = result.Message;
                return RedirectToPage("/Product", new { id = productSlug });
            }
            return RedirectToPage("/Product", new { id = productSlug });
        }
    }
}
