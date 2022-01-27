using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : EntityBase
    {
        public string Title { get; private set; }
        public string ShortDescription { get; private set; }
        public string Content { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public bool IsRemoved { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory ArticleCategory { get; private set; }

        public Article(string title, string shortDescription, string content,
            string picture, string pictureAlt, string pictureTitle,
            DateTime publishDate, string slug, string keywords,
            string metaDescription, string canonicalAddress, long categoryId)
        {
            Title = title;
            ShortDescription = shortDescription;
            Content = content;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            PublishDate = publishDate;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            IsRemoved = false;
            CategoryId = categoryId;
        }

        public void Edit(string title, string shortDescription, string content,
            string picture, string pictureAlt, string pictureTitle,
            DateTime publishDate, string slug, string keywords,
            string metaDescription, string canonicalAddress, long categoryId)
        {
            Title = title;
            ShortDescription = shortDescription;
            Content = content;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            PublishDate = publishDate;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            CategoryId = categoryId;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
