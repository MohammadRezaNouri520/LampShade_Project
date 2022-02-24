using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="کد")]
        public string Code { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="توضیحات کوتاه")]
        public string ShortDescription { get; set; }

        [Display(Name="توضیحات")]
        public string Description { get; set; }

        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtensionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.FileExtension)]
        [Display(Name="عکس")]
        public IFormFile Picture { get; set; }

        [Display(Name="Alt عکس")]
        public string PictureAlt { get; set; }
        
        [Display(Name="عنوان عکس")]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="کلمات کلیدی")]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="توضیحات متا")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="اسلاگ")]
        public string Slug { get; set; }

        [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name="گروه محصول")]
        public long CategoryId { get; set; }

        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
