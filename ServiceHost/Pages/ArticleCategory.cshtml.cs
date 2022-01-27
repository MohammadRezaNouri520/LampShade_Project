using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }

        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string id)
        {
            ArticleCategory = _articleCategoryQuery.GetArticleCategoryDetails(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategoryNames();
            LatestArticles = _articleQuery.GetLatestArticles();
        }
    }
}
