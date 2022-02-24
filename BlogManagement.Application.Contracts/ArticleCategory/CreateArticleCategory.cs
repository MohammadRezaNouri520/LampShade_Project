using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="نام")]
        public string Name { get; set; }

        [Display(Name ="عکس")]
        public IFormFile Picture { get; set; }
        
        [Display(Name ="Alt عکس")]
        public string PictureAlt { get; set; }
        
        [Display(Name ="عنوان عکس")]
        public string PictureTitle { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="توضیحات")]
        public string Description { get; set; }

        [Range(1,long.MaxValue,ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="ترتیب نمایش")]
        public int ShowOrder { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="اسلاگ")]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="کلمات کلیدی")]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="توضیحات متا")]
        public string MetaDescription { get; set; }
        
        [Display(Name ="Canonical Address")]
        public string CanonicalAddress { get; set; }
    }
}
