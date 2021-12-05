using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long, Inventory>
    {
        Inventory GetBy(long productId);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        List<InventoryOperationViewModel> GetInventoryOperations(long id);
    }
}
