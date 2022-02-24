using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name="نام")]
        public string Name { get; set; }

        [Display(Name="توضیحات")]
        public string Description { get; set; }

        //[Required(ErrorMessage = ValidationMessages.IsRequired)]
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
    }
}
