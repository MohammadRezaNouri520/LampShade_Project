using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {
        [MaxFileSize(10 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
        [FileExtensionLimitation(new string[] {".jpg",".jpeg",".png"}, ErrorMessage = ValidationMessages.FileExtension)]
        [Display(Name ="عکس")]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name ="Alt عکس")]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessages.MaxLength)] 
        [Display(Name ="عنوان عکس")]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name ="سر تیتر")]
        public string Heading { get; set; }

        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLength)] 
        [Display(Name ="عنوان")]
        public string Title { get; set; }

        [MaxLength(255, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name ="متن")]
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(50, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name ="متن دکمه")]
        public string BtnText { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(1000, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name ="لینک")]
        public string Link { get; set; }
    }
}
