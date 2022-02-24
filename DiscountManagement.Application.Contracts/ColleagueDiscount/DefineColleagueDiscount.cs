using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public class DefineColleagueDiscount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="محصول")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, 100, ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="درصد تخفیف")]
        public int DiscountRate { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
