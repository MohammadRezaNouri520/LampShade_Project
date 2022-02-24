using _0_Framework.Application;
using ShopManagement.Application.Contracts.Product;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage =ValidationMessages.IsRequired)]
        [Display(Name ="محصول")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        [Display(Name ="قیمت واحد")]
        public double UnitPrice { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
