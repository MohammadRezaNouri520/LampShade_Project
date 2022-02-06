using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace CommentManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage =ValidationMessages.MaxLength)]
        [Display(Name ="نام")]
        public string Name { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [MaxLength(500, ErrorMessage =ValidationMessages.MaxLength)]
        [DataType(DataType.EmailAddress, ErrorMessage =ValidationMessages.EmailFormat)]
        [Display(Name ="ایمیل")]
        public string Email { get; set; }
        
        [MaxLength(500, ErrorMessage =ValidationMessages.MaxLength)]
        [Display(Name ="وب سایت")]
        public string Website { get; set; }
        
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [MaxLength(1000, ErrorMessage =ValidationMessages.MaxLength)]
        [Display(Name ="نظر شما")]
        public string Message { get; set; }
        
        public long OwnerRecordId { get; set; }
        
        public int Type { get; set; }
        
        [Range(0,long.MaxValue)]
        public long ParentId { get; set; }
    }
}
