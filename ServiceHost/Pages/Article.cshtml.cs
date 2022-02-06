using _01_LampshadeQuery;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        [BindProperty]
        public AddComment NewComment { get; set; }
        
        public ArticleQueryModel Article { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }

        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetails(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategoryNames();
            LatestArticles = _articleQuery.GetLatestArticles();
        }

        public IActionResult OnPost(AddComment newComment, string articleSlug)
        {
            if (ModelState.IsValid)
            {
                newComment.Type = CommentType.Article;
                var result = _commentApplication.Add(newComment);
            }
            return RedirectToPage("/Article", new { id = articleSlug });
        }
    }
}
