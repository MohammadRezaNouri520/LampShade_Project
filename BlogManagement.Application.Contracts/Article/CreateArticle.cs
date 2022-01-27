using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "عنوان مقاله")]
        [MaxLength(500, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(1000, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "محتوای مقاله")]
        public string Content { get; set; }

        [Display(Name = "عکس")]
        public IFormFile Picture { get; set; }

        [Display(Name = "Alt عکس")]
        [MaxLength(500, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان عکس")]
        [MaxLength(500, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "تاریخ انتشار")]
        public string PublishDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "اسلاگ")]
        [MaxLength(600, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "کلمات کلیدی")]
        [MaxLength(100, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "توضیحات متا")]
        [MaxLength(150, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string MetaDescription { get; set; }

        [Display(Name = "Canonical Address")]
        [MaxLength(1000, ErrorMessage = "حداکثر {0} کاراکتر وارد کنید")]
        public string CanonicalAddress { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "گروه مقاله")]
        public long CategoryId { get; set; }
    }
}
