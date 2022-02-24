using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        
        [Range(1,long.MaxValue,ErrorMessage =ValidationMessages.IsRequired)]
        [Display(Name ="محصول")]
        public long ProductId { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Range(1,100,ErrorMessage =ValidationMessages.IsRequired)]
        [Display(Name ="درصد تخفیف")]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Display(Name ="تاریخ شروع")]
        public string StartDate { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Display(Name ="تاریخ پایان")]
        public string EndDate { get; set; }
        
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Display(Name ="علت تخفیف")]
        public string Description { get; set; }
        
        public List<ProductViewModel> Products { get; set; }
    }
}
