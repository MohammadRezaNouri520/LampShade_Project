using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using System.Collections.Generic;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }


        public OperationResult Create(CreateArticleCategory command)
        {
            var operation = new OperationResult();
            
            if (_articleCategoryRepository.Exist(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var path = $"Blog/ArticleCategories/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            var articleCategory = new ArticleCategory(command.Name, command.Description,
                picturePath, command.PictureAlt, command.PictureTitle, command.ShowOrder,
                slug, command.Keywords, command.MetaDescription, command.CanonicalAddress);
            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            var articleCategory = _articleCategoryRepository.Get(command.Id);

            if (articleCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var path = $"Blog/ArticleCategories/{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);
            articleCategory.Edit(command.Name, command.Description, picturePath,
                command.PictureAlt, command.PictureTitle, command.ShowOrder, slug,
                command.Keywords, command.MetaDescription, command.CanonicalAddress);

            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }
    }
}
