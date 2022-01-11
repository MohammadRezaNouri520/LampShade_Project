using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var categoriesWithProducts = _shopContext.ProductCategories
                .Include(x => x.Products)
                //.ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = x.Products.Select(p => new ProductQueryModel  //// MapProducts(x => x.Products)
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Category = x.Name,
                        Picture = p.Picture,
                        PictureAlt = p.PictureAlt,
                        PictureTitle = p.PictureTitle,
                        Slug = p.Slug
                        //,
                        //Price = _inventoryContext.Inventory.FirstOrDefault(i => i.ProductId == p.Id).UnitPrice.ToString()
                    }).ToList()
                }).OrderByDescending(x => x.Id).ToList();

            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.IsActive)
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            foreach (var category in categoriesWithProducts)
            {
                foreach (var product in category.Products)
                {
                    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productInventory != null)
                    {
                        var Price = productInventory.UnitPrice;
                        product.Price = Price.ToString("#,0");

                        var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                        if (productDiscount != null)
                        {
                            var DiscountRate = productDiscount.DiscountRate;
                            product.DiscountRate = DiscountRate;

                            product.PriceWithDiscount = CalculateDiscountedPrice(Price, DiscountRate).ToString("#,0");
                            product.HasDiscount = product.DiscountRate > 0;
                        }

                    }

                }
            }
            return categoriesWithProducts;
        }

        private static List<ProductQueryModel> MapProducts(IEnumerable<Product> products)
        {
            return products.Select(x => new ProductQueryModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).OrderByDescending(x => x.Id).ToList();
        }

        //private static List<ProductQueryModel> MapProducts(List<Product> products)
        //{
        //    var result = new List<ProductQueryModel>();
        //    // foreach loop:
        //    foreach (var product in products)
        //    {
        //        result.Add(new ProductQueryModel
        //        {
        //            Id = product.Id,
        //            Name = product.Name,
        //            Category = product.Category.Name,
        //            Picture = product.Picture,
        //            PictureAlt = product.PictureAlt,
        //            PictureTitle = product.PictureTitle,
        //            Slug = product.Slug
        //        });
        //    }

        //    // for loop:
        //    for (int i = 0; i < products.Count; i++)
        //    {
        //        result.Add(new ProductQueryModel
        //        {
        //            Id = products[i].Id,
        //            Name = products[i].Name,
        //            Category = products[i].Category.Name,
        //            Picture = products[i].Picture,
        //            PictureAlt = products[i].PictureAlt,
        //            PictureTitle = products[i].PictureTitle,
        //            Slug = products[i].Slug
        //        });
        //    }
        //    return result;
        //}
        
        
        public ProductCategoryQueryModel GetProductCategoriesWithProductsBy(string slug)
        {
            var categoryWithProducts = _shopContext.ProductCategories
                .Select(x => new ProductCategoryQueryModel 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Products = x.Products.Select(p => new ProductQueryModel 
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Category = x.Name,
                        Picture = p.Picture,
                        PictureAlt=p.PictureAlt,
                        PictureTitle=p.PictureTitle,
                        Slug = p.Slug
                    }).ToList()
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            var inventory = _inventoryContext.Inventory.Select(i => new {i.ProductId, i.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(d => d.IsActive)
                .Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                .Select(d => new { d.ProductId, d.DiscountRate, d.EndDate }).ToList();

            foreach (var product in categoryWithProducts.Products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                if(productInventory != null)
                {
                    var Price = productInventory.UnitPrice;
                    product.Price = Price.ToString("#,0");

                    var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if(productDiscount != null)
                    {
                        var DiscountRate = productDiscount.DiscountRate;
                        product.DiscountRate = DiscountRate;
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                        product.PriceWithDiscount = CalculateDiscountedPrice(Price, DiscountRate).ToString("#,0");
                        product.HasDiscount = product.DiscountRate > 0;
                    }
                }
            }
            return categoryWithProducts;
        }


        private static double CalculateDiscountedPrice(double price, double discountRate)
        {
            var discountedAmount = Math.Round(price * discountRate / 100);
            return price - discountedAmount;
        }
    }
}
