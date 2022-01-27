using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public ArticleSearchModel SearchModel { get; set; }
        public List<ArticleViewModel> Articles { get; set; }
        public SelectList ArticleCategories { get; set; }


        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleApplication articleApplication, IArticleCategoryApplication articleCategoryApplication)
        {
            _articleApplication = articleApplication;
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetArticleCategoriesSelectList(), "Id", "Name");
            Articles = _articleApplication.Search(searchModel);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _articleApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _articleApplication.Restore(id);
            return RedirectToPage("./Index");

        }
    }
}
