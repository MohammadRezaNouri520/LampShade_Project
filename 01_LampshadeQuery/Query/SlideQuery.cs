using _01_LampshadeQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides
                .Where(s => s.IsRemoved == false)
                .Select(s => new SlideQueryModel
                {
                    Picture = s.Picture,
                    PictureAlt = s.PictureAlt,
                    PictureTitle = s.PictureTitle,
                    Heading = s.Heading,
                    Title = s.Title,
                    Text = s.Text,
                    Link = s.Link,
                    BtnText = s.BtnText
                }).ToList();
        }
    }
}
