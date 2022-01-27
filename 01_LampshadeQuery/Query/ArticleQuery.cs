using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _blogContext.Articles
                 .Include(x => x.ArticleCategory)
                 .Where(x => !x.IsRemoved)
                 .Where(x => x.PublishDate <= DateTime.Now)
                 .Select(x => new ArticleQueryModel
                 {
                     Slug = x.Slug,
                     Title = x.Title,
                     Category = x.ArticleCategory.Name,
                     CategorySlug = x.ArticleCategory.Slug,
                     ShortDescription = x.ShortDescription,
                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle,
                     Content = x.Content,
                     PublishDate = x.PublishDate.ToFullPersianDate(),
                     Keywords = x.Keywords,
                     MetaDescription = x.MetaDescription
                 })
                 .AsNoTracking()
                 .FirstOrDefault(x => x.Slug == slug);

            if (!string.IsNullOrWhiteSpace(article.Keywords))
                article.KeywordsList = article.Keywords.Split("،").ToList();

            return article;
        }

        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles
                .Where(x => !x.IsRemoved)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    PublishMonth = x.PublishDate.ToPersianMonth(),
                    PublishDay = x.PublishDate.ToPersianDay(),
                    PublishDate = x.PublishDate.ToFullPersianDate(),
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                })
                .OrderByDescending(x => x.Id)
                .Take(5)
                .AsNoTracking()
                .ToList();
        }
    }
}
