using _0_Framework.Application;
using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(DecreaseInventory command);
        OperationResult Reduce(List<DecreaseInventory> commands);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);

        List<InventoryOperationViewModel> GetInventoryOpertaions(long id);

    }
}
