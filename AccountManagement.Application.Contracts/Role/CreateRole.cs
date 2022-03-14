using _0_Framework.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Role
{
    public class CreateRole
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        public List<int> Permissions { get; set; }
    }
}
