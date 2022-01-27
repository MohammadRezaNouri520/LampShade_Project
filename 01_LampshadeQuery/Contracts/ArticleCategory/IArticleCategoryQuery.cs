using System.Collections.Generic;

namespace _01_LampshadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ArticleCategoryQueryModel GetArticleCategoryDetails(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategoryNames();
    }
}
