using _01_LampshadeQuery;
using _01_LampshadeQuery.Contracts.ArticleCategory;
using _01_LampshadeQuery.Contracts.ProductCategory;
using BlogManagement.Application.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IArticleCategoryQuery articleCategoryQuery, IProductCategoryQuery productCategoryQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke() 
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategoryNames(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };
            return View(result);
        }
    }
}
