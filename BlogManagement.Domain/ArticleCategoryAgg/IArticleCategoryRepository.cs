using _0_Framework.Domain;
using BlogManagement.Application.Contracts.ArticleCategory;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        string GetArticleCategorySlugBy(long id);
        List<ArticleCategoryViewModel> GetArticleCategoriesSelectList();
    }
}
