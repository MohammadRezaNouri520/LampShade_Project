using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product:EntityBase
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDesctiption { get; private set; }
        public string Slug { get; private set; }

        public long CategoryId { get; private set; }
        public ProductCategory Category { get; private set; }
        public List<ProductPicture> ProductPictures { get; private set; }


        public Product(string name, string code, string shortDescription,
            string description, string picture, string pictureAlt, string pictureTitle,
            string keywords, string metaDesctiption, string slug, long categoryId)
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDesctiption = metaDesctiption;
            Slug = slug;
            CategoryId = categoryId;
        }

        public void Edit(string name, string code, string shortDescription,
            string description, string picture, string pictureAlt, string pictureTitle,
            string keywords, string metaDesctiption, string slug, long categoryId) 
        {
            Name = name;
            Code = code;
            ShortDescription = shortDescription;
            Description = description;
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDesctiption = metaDesctiption;
            Slug = slug;
            CategoryId = categoryId;
        }
    }
}
