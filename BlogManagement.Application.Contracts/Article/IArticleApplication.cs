using _0_Framework.Application;
using System.Collections.Generic;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);
        OperationResult Edit(EditArticle command);
        EditArticle GetDetails(long id);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
