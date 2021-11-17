using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public class ProductPicture:EntityBase
    {
        public string Picture { get; private set; }
        public string PicutreAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }

        public long ProductId { get; private set; }
        public Product Product { get; private set; }

        public ProductPicture(string picture, string picutreAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PicutreAlt = picutreAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
            IsRemoved = false;
        }

        public void Edit(string picture, string picutreAlt, string pictureTitle, long productId)
        {
            Picture = picture;
            PicutreAlt = picutreAlt;
            PictureTitle = pictureTitle;
            ProductId = productId;
            IsRemoved = false;
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
