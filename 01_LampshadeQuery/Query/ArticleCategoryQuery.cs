using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleCategoryQueryModel GetArticleCategoryDetails(string slug)
        {
            var articleCategory = _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    ArticlesCount = x.Articles.Count(a => !a.IsRemoved),
                    Articles = x.Articles
                        .Where(a => !a.IsRemoved)
                        .Where(a => a.PublishDate <= DateTime.Now)
                        .Select(a => new ArticleQueryModel 
                        {
                            Id=a.Id,
                            Title=a.Title,
                            ShortDescription=a.ShortDescription,
                            PublishDate=a.PublishDate.ToFullPersianDate(),
                            Picture=a.Picture,
                            PictureAlt=a.PictureAlt,
                            PictureTitle=a.PictureTitle,
                            Slug=a.Slug
                        })
                        .OrderByDescending(a => a.Id)
                        .ToList()
                })
                .AsNoTracking()
                .FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(articleCategory.Keywords))
                articleCategory.KeywordsList = articleCategory.Keywords.Split("،").ToList();

            return articleCategory;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoryNames()
        {
            return _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel
                {
                    Name = x.Name,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ArticlesCount = x.Articles.Count(a => !a.IsRemoved && a.PublishDate <= DateTime.Now)
                })
                .AsNoTracking()
                .ToList();
        }
    }
}
