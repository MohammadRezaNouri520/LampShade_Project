using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class DecreaseInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        [Range(1,long.MaxValue, ErrorMessage =ValidationMessages.IsRequired)]
        public long Count { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public string Description { get; set; }
        public long OrderId { get; set; }
    }
}
