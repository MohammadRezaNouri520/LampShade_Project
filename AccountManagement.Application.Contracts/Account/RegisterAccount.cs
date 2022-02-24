using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account
{
    public class RegisterAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name ="نام کامل")]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(20, ErrorMessage = ValidationMessages.MaxLength)]
        [DataType(DataType.PhoneNumber,ErrorMessage =ValidationMessages.InvalidPhoneNumber)]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = ValidationMessages.PasswordNotMatch)]
        public string RePassword { get; set; }

        //[Range(1, long.MaxValue,ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name = "نقش کاربر")]
        public long RoleId { get; set; }

        [Display(Name = "تصویر پروفایل")]
        public IFormFile ProfilePhoto { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
