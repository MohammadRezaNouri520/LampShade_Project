using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;
        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public EditProductCategory GetDetails(long id)
        {
            return _context.ProductCategories.Select(p => new EditProductCategory 
            {
                Id=p.Id,
                Name=p.Name,
                Description=p.Description,
                Picture=p.Picture,
                PictureAlt=p.PictureAlt,
                PictureTitle=p.PictureTitle,
                Keywords=p.Keywords,
                MetaDescription=p.MetaDescription,
                Slug=p.Slug
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query = _context.ProductCategories.Select(p => new ProductCategoryViewModel
            { 
                Id=p.Id,
                Name=p.Name,
                Picture=p.Picture,
                CreationDate=p.CreationDate.ToString("yyyy/MM/dd")
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
