using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(s => new EditSlide 
            {
                Id=s.Id,
                Picture=s.Picture,
                PictureAlt=s.PictureAlt,
                PictureTitle=s.PictureTitle,
                Heading=s.Heading,
                Title=s.Title,
                Text=s.Text,
                BtnText=s.BtnText
            }).FirstOrDefault(s => s.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(s => new SlideViewModel 
            {
                Id=s.Id,
                Picture=s.Picture,
                Heading=s.Heading,
                Title=s.Title,
                IsRemoved=s.IsRemoved,
                CreationDate=s.CreationDate.ToString("yyyy/MM/dd")
            }).OrderByDescending(s => s.Id).ToList();
        }
    }
}
