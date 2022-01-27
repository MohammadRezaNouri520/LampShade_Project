using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IFileUploader fileUploader, IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository)
        {
            _fileUploader = fileUploader;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exist(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetArticleCategorySlugBy(command.CategoryId);
            var path = $"Blog/ArticleCategories/{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var article = new Article(command.Title, command.ShortDescription,
                command.Content, picturePath, command.PictureAlt, command.PictureTitle,
                publishDate, slug, command.Keywords, command.MetaDescription, 
                command.CanonicalAddress, command.CategoryId);

            _articleRepository.Create(article);
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.GetArticleWithCategoryBy(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var publishDate = command.PublishDate.ToGeorgianDateTime();
            var slug = command.Slug.Slugify();
            var categorySlug = article.ArticleCategory.Slug;
            var path = $"Blog/ArticleCategories/{categorySlug}/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            article.Edit(command.Title, command.ShortDescription, command.Content,
                picturePath, command.PictureAlt, command.PictureTitle, publishDate, slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.SaveChanges();
            return operation.Succeeded();

        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var article = _articleRepository.Get(id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            article.Remove();
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var article = _articleRepository.Get(id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            article.Restore();
            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
