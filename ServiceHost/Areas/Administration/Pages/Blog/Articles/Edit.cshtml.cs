using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class EditModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public EditModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        [BindProperty]
        public EditArticle Article { get; set; }
        
        public SelectList ArticleCategories
        {
            get
            {
                return new SelectList(_articleCategoryApplication.GetArticleCategoriesSelectList(), "Id", "Name");
            }
        }

        
        public void OnGet(long id)
        {
            Article = _articleApplication.GetDetails(id);
            //ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategoriesSelectList(), "Id", "Name", Article?.CategoryId);
        }

        public IActionResult OnPost(EditArticle article)
        {
            if (ModelState.IsValid)
            {
                var result = _articleApplication.Edit(article);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
