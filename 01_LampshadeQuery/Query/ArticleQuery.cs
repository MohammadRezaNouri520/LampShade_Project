using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Article;
using _01_LampshadeQuery.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _blogContext.Articles
                 .Include(x => x.ArticleCategory)
                 .Where(x => !x.IsRemoved)
                 .Where(x => x.PublishDate <= DateTime.Now)
                 .Select(x => new ArticleQueryModel
                 {
                     Id=x.Id,
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

            var comments = _commentContext.Comments
                .Where(c => c.OwnerRecordId == article.Id)
                .Where(c => c.Type == CommentType.Article)
                .Where(c => !c.IsCanceled && c.IsConfirmed)
                .Select(c => new CommentQueryModel 
                {
                    Id=c.Id,
                    Name=c.Name,
                    Message=c.Message,
                    CreationDate=c.CreationDate.ToFullPersianDate(),
                    ParentId=c.ParentId
                })
                .OrderByDescending(c => c.Id)
                .ToList();

            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                    comment.ParentName = _commentContext.Comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            }

            article.Comments = comments;

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
