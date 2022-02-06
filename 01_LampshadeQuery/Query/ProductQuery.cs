using _0_Framework.Application;
using _01_LampshadeQuery.Contracts.Comment;
using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var products = _shopContext.Products.Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                })
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .Take(6)
                .ToList();

            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.IsActive)
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            foreach (var product in products)
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

                        //static double CalculateDiscountedPrice(double price, double discountRate)
                        //{
                        //    var discountedAmount = Math.Round(price * discountRate / 100);
                        //    return price - discountedAmount;
                        //}

                    }
                }
            }

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice }).AsNoTracking().ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.IsActive)
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .AsNoTracking()
                .ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                });

            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            var products = query.OrderByDescending(x => x.Id).AsNoTracking().ToList();

            foreach (var product in products)
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
                        product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                    }
                }
            }
            return products;
        }


        public ProductQueryModel GetProductDetails(string slug)
        {
            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    CategorySlug = x.Category.Slug,
                    Slug = x.Slug,
                    Code = x.Code,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDesctiption,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Pictures = x.ProductPictures
                        .Where(p => !p.IsRemoved)
                        .Select(p => new ProductPictureQueryModel
                        {
                            ProductId = x.Id,
                            Picture = p.Picture,
                            PicutreAlt = p.PicutreAlt,
                            PictureTitle = p.PictureTitle,
                            IsRemoved = p.IsRemoved
                        }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefault(x => x.Slug == slug);

            var productInventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice, x.IsInStock }).FirstOrDefault(x => x.ProductId == product.Id);
            var productDiscount = _discountContext.CustomerDiscounts
                .Where(x => x.IsActive)
                .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory != null)
            {
                var Price = productInventory.UnitPrice;
                product.Price = Price.ToString("#,0");
                product.IsInStock = productInventory.IsInStock;

                if (productDiscount != null)
                {
                    var discountRate = productDiscount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.PriceWithDiscount = CalculateDiscountedPrice(Price, discountRate).ToString("#,0");
                    product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();
                    product.HasDiscount = product.DiscountRate > 0;
                }
            }

            product.Comments = _commentContext.Comments
                        .Where(c => c.OwnerRecordId == product.Id)
                        .Where(c => c.Type == CommentType.Product)
                        .Where(c => !c.IsCanceled && c.IsConfirmed)
                        .Select(c => new CommentQueryModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Message = c.Message
                        }).OrderByDescending(c => c.Id).ToList();

            return product;
        }


        private static double CalculateDiscountedPrice(double price, double discountRate)
        {
            var discountedAmount = Math.Round(price * discountRate / 100);
            return price - discountedAmount;
        }
    }
}
