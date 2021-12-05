using System;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class InventoryOperation
    {
        public long Id { get; private set; }
        public DateTime OperationDate { get; private set; }
        public bool Operation { get; private set; }
        public long OperatorId { get; private set; }
        public long Count { get; private set; }
        public long OrderId { get; private set; }
        public string Description { get; private set; }
        public long CurrentCount { get; private set; }

        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }

        public InventoryOperation(bool operation, long operatorId,
            long count, long orderId, string description,
            long currentCount, long inventoryId)
        {
            Operation = operation;
            OperatorId = operatorId;
            Count = count;
            OrderId = orderId;
            Description = description;
            CurrentCount = currentCount;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }
}
