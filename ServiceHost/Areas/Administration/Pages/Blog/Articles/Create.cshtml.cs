using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public CreateModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }


        [BindProperty]
        public CreateArticle Article { get; set; }
        public SelectList ArticleCategories
        {
            get 
            {
                return new SelectList(_articleCategoryApplication.GetArticleCategoriesSelectList(), "Id", "Name");
            }
        }

        public void OnGet()
        {
            Article = new CreateArticle();
            //ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategoriesSelectList(), "Id", "Name");
        }

        public IActionResult OnPost(CreateArticle article)
        {
            if (ModelState.IsValid)
            {
                var result = _articleApplication.Create(article);
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
