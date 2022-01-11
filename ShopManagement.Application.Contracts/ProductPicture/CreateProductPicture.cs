using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, long.MaxValue, ErrorMessage =ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [MaxFileSize(3*1024*1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtensionLimitation(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = ValidationMessages.FileExtension)]
        public IFormFile Picture { get; set; }
        
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
       
        public List<ProductViewModel> Products { get; set; }
    }
}
