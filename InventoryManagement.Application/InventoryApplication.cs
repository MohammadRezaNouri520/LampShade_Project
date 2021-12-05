using _0_Framework.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Application
{
    public class InventoryApplication : IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }


        public OperationResult Create(CreateInventory command)
        {
            var operation = new OperationResult();
            if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(DecreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 1;
            inventory.Reduce(operatorId, command.Count, command.Description, 0);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<DecreaseInventory> commands)
        {
            var operation = new OperationResult();
            const long operatorId = 1;
            foreach (var command in commands)
            {
                var inventory = _inventoryRepository.Get(command.InventoryId);
                inventory.Reduce(operatorId, command.Count, command.Description, command.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_inventoryRepository.Exist(x => x.ProductId == command.ProductId && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryRepository.GetDetails(id);
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            const long operatorId = 0;
            inventory.Increase(operatorId, command.Count, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            return _inventoryRepository.Search(searchModel);
        }

        public List<InventoryOperationViewModel> GetInventoryOpertaions(long id)
        {
            return _inventoryRepository.GetInventoryOperations(id);
        }
    }
}
