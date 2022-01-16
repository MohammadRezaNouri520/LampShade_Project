using System.Collections.Generic;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }

        public ArticleCategorySearchModel SearchModel { get; set; }
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }

        public void OnGet(ArticleCategorySearchModel searchModel)
        {
            ArticleCategories = _articleCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateArticleCategory();
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateArticleCategory command)
        {
            if(ModelState.IsValid)
            {
                var result = _articleCategoryApplication.Create(command);
                return new JsonResult(result);
            }
            return new JsonResult(false);
        }

        public IActionResult OnGetEdit(long id)
        {
            var command = _articleCategoryApplication.GetDetails(id);
            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditArticleCategory command)
        {
            if(ModelState.IsValid)
            {
                var result = _articleCategoryApplication.Edit(command);
                return new JsonResult(result);
            }
            return new JsonResult(false);
        }       
    }
}
